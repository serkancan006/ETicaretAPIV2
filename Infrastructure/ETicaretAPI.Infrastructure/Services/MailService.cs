using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService(IConfiguration configuration) : IMailService
    {
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(configuration["Mail:Username"], "ScKaya E-Ticaret", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(configuration["Mail:Username"], configuration["Mail:Password"]);
            smtp.Port = Convert.ToInt32(configuration["Mail:Port"]);
            smtp.EnableSsl = Convert.ToBoolean(configuration["Mail:useSsl"]);
            smtp.Host = configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(configuration["ClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>NG - ScKaya E-Ticaret");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Sayın {userName} Merhaba<br>" +
                $"{orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu siparişiniz tamamlanmış ve kargo firmasına verilmiştir.<br>Hayrını görünüz efendim...";

            await SendMailAsync(to, $"{orderCode} Sipariş Numaralı Siparişiniz Tamamlandı", mail);

        }

    }
}
