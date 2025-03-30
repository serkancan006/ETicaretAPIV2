using ETicaretAPI.Application.Abstractions.Encrypt;
using ETicaretAPI.Application.Abstractions.Helpers;
using ETicaretAPI.Application.Abstractions.Payment;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Sms;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Infrastructure.Enums;
using ETicaretAPI.Infrastructure.Helpers;
using ETicaretAPI.Infrastructure.Services;
using ETicaretAPI.Infrastructure.Services.Encrypt;
using ETicaretAPI.Infrastructure.Services.Payments;
using ETicaretAPI.Infrastructure.Services.Payments.Iyzico;
using ETicaretAPI.Infrastructure.Services.Sms;
using ETicaretAPI.Infrastructure.Services.Storage;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Infrastructure.Services.Storage.Minio;
using ETicaretAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IHelpersInfrastructure, HelpersInfrastructure>();
            serviceCollection.AddScoped<IEncryptionService, EncryptionService>();
            serviceCollection.AddScoped<ISmsService, SmsService>();
            // --------------------------------------------------------------------
            // Kapıda ödeme servisi
            serviceCollection.AddScoped<IPaymentService, CashOnDeliveryPaymentService>();

            // Online Ödeme ödeme servisi
            serviceCollection.AddScoped<IPaymentService, OnlinePaymentService>();

            // Online ödeme servisi (Burada Iyzico veya PayPal seçilir)
            serviceCollection.AddSingleton<IyzicoClient>();     
            serviceCollection.AddScoped<IOnlinePaymentGateway, IyzicoService>();     // IyzicoPaymentGateway
            // builder.Services.AddScoped<IOnlinePaymentGateway, PayPalPaymentGateway>(); // İsterseniz bunu aktif edin
            

        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                case StorageType.Minio:
                    serviceCollection.AddScoped<IStorage, MinioStorage>();
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
