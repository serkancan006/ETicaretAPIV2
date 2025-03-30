using Iyzipay;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Infrastructure.Services.Payments.Iyzico
{
    public class IyzicoClient
    {
        private readonly Options _options;

        public IyzicoClient(IConfiguration configuration)
        {
            _options = new Options
            {
                ApiKey = configuration["Payment:Iyzico:ApiKey"],
                SecretKey = configuration["Payment:Iyzico:SecretKey"],
                BaseUrl = configuration["Payment:Iyzico:BaseUrl"]
            };
        }

        public Options GetOptions()
        {
            return _options;
        }
       
    }
}
