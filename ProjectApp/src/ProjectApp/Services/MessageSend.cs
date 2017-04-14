using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectApp.Services
{
    public class MessageSend : IEmailSend
    {
        public MessageSend(IOptions<MessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public MessageSenderOptions Options { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new MailAddress("nanner_man@live.com", "Application");
            myMessage.Subject = subject;
            myMessage.Text = message;

            var apiKey = Options.SendGridApiKey;
            var transportWeb = new Web(apiKey);

            return transportWeb.DeliverAsync(myMessage);
        }
    }
}
