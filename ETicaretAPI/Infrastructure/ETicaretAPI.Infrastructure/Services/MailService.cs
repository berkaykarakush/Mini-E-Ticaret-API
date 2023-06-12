using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] {to}, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            foreach(var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            mail.From = new(_configuration["Mail:Username"], "Mini E-Ticaret", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = int.Parse(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMail(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.Append("Merhaba<br>Eger yeni sifre talebinde bulunduysaniz asagidaki linkten sifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.Append(_configuration["AngularClientUrl"]);
            mail.Append("/update-password/");
            mail.Append(userId);
            mail.Append("/");
            mail.Append(resetToken);
            mail.Append("\">Yeni sifre talebi icin tiklayiniz.</a></strong>");
            mail.Append("<br><br><br><br>Not: Eger bu talep tarafinizca gerceklestirilmediyse lutfen bu maili dikkate   almayiniz.<br><br><br>Saygilarimizla<br>Mini E-Ticaret");
            await SendMailAsync(to, "Sifre Yenileme Talebi", mail.ToString());
        }
        public async Task<(bool, CompletedOrderDTO)> SendCompleteOrderMailAsync(string to, string orderCode, DateTime orderDate, string username)
        {
            string mail = $"Sayin {username} Merhaba<br>{orderDate} tarihinde olusturulan {orderCode} kodlu siparisiniz kargoya verilmistir.<br>Saygilarimizla<br>Mini E-Ticaret";
            await SendMailAsync(to, $"{orderCode}'lu Siparisiniz Kargoya Verildi!", mail);
            return new();
        }
    }
}
