using ChiefsCorner.Management.Service.Models;

namespace ChiefsCorner.Management.Service.Services
{
    public interface IEmailService
    {
        void SendEmail(MailMessage mailMessage);
    }
}
