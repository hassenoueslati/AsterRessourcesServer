using BackOfficeAPI.Models;

namespace BackOfficeAPI.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailModel model);
    }
}
