using ETicaretAPI.API.Extensions;
using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;
using ETicaretAPI.SignalR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
// Layers
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
// Storages
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage();
// Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins(builder.Configuration["ClientUrl"]).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

#region Logs
Logger log = new LoggerConfiguration()
    .WriteTo.Async(p => p.Console())
    .WriteTo.Async(p => p.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("MsSQL"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "logs",
            AutoCreateSqlTable = true
        },
        columnOptions: new ColumnOptions
        {
            AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn("user_name", SqlDbType.VarChar)
            }
        }))
    .WriteTo.Async(p => p.Seq(builder.Configuration["Seq:ServerURL"]))
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
#endregion

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    // b�t�n controllerlar [Authorize] gibi oldu Authentication olmayan controller i�in kullan�lmal� = [AllowAnonymous]
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
})
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true); // model validasyonlar� devre d��� b�rakt�k yan�t d�nd�rme olarak.

#region Validation
// FluentValidation'� ekleyin
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

// Validator'lar� kaydedin
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    // Define the BearerAuth scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.  
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�erdir. -> www.bilmemne.com
            ValidateIssuer = true, //Olu�turulacak token de�erini kimin da��tt�n� ifade edece�imiz aland�r. -> www.myapi.com
            ValidateLifetime = true, //Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r.
            ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden suciry key verisinin do�rulanmas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            // ClockSkew = TimeSpan.Zero,  // zamanlar� e�itle
            NameClaimType = ClaimTypes.Name //JWT �zerinde Name claimne kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.
        };
    });

// Authorization policy'lerini ekliyoruz
builder.Services.AddAuthorization(options =>
{
    // Admin rol�ne sahip kullan�c�lar i�in bir policy tan�ml�yoruz
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));

    // User rol�ne sahip kullan�c�lar i�in bir policy tan�ml�yoruz
    options.AddPolicy("UserPolicy", policy =>
        policy.RequireRole("User"));

    // [Authorize(Policy = "AdminOrUserPolicy")] or [Authorize(Roles = "AdminOrUserPolicy")
    // Hem Admin hem de User rollerine sahip olanlar i�in policy tan�mlayal�m
    options.AddPolicy("AdminOrUserPolicy", policy =>
        policy.RequireRole("Admin", "User"));

    // [Authorize(Policy = "NoRolesPolicy")]
    // Rols�z kullan�c�lar i�in policy
    options.AddPolicy("NoRolesPolicy", policy =>
        policy.RequireAssertion(context =>
            !context.User.Claims.Any(c => c.Type == ClaimTypes.Role))); // Hi�bir rol�n olmamas� durumu

    // [Authorize]
    // Genel yetkilendirme: Hem rols�z hem de roller atanm�� kullan�c�lar
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
    app.UseHsts();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>()); // Glocal Exception Handler
app.UseStaticFiles(); // Files

app.UseSerilogRequestLogging(); // Serilog

app.UseHttpLogging();
app.UseCors(); // Cors
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context?.User?.Identity?.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
}); // Custom Midlleware

app.MapControllers();
app.MapHubs(); // SignalR Hubs

app.Run();
