using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using OnlineStore.Models.WebModels.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace OnlineStore.Services.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private readonly MessageSenderOptions options;

        public EmailSender(IOptions<MessageSenderOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = options.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = htmlMessage;
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
