using System.Text;
using ETicaretAPI.Application.Abstractions.Sms;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Infrastructure.Services.Sms
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SmsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("NetgsmClient");
            _configuration = configuration;
        }

        public async Task<string> SendSmsAsync(string phoneNumber, string message, string messageTitle)
        {
            try
            {
                // XML Formatı
                var xml = $@"<?xml version='1.0' encoding='UTF-8'?>
                                <mainbody>
                                    <header>
                                        <usercode>{_configuration["NetGsm:UserCode"]}</usercode>
                                        <password>{_configuration["NetGsm:Password"]}</password>
                                        <msgheader>{messageTitle}</msgheader>
                                        <appkey>{_configuration["NetGsm:AppKey"]}</appkey>
                                    </header>
                                    <body>
                                        <msg><![CDATA[{message}]]></msg>
                                        <no>{phoneNumber}</no>
                                    </body>
                                </mainbody>";

                var content = new StringContent(xml, Encoding.UTF8, "application/x-www-form-urlencoded");

                // POST isteği gönder
                var response = await _httpClient.PostAsync(string.Empty, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return $"Başarılı: {result}";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return $"Hata: {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Beklenmedik Hata: {ex.Message}";
            }
        }
    }
}
