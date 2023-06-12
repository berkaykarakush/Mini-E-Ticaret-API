using ETicaretAPI.Application.DTOs.Order;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMail(string to, string userId, string resetToken);
        Task<(bool, CompletedOrderDTO)> SendCompleteOrderMailAsync(string to, string orderCode, DateTime orderDate, string username);
    }
}
