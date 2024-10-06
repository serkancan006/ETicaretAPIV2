using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager) : ITokenHandler
    {
        public async Task<Application.DTOs.Token> CreateAccessTokenAsync(int second, AppUser user)
        {
            Application.DTOs.Token token = new();

            //Security Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> { new(ClaimTypes.Name, user.UserName) };
            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Count > 0)
            {
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }
            }

            //Oluşturulacak token ayarlarını veriyoruz.
            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: configuration["Token:Audience"],
                issuer: configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            //Token oluşturucu sınıfından bir örnek alalım.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            //string refreshToken = CreateRefreshToken();

            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
