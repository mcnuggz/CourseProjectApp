using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApp.Services
{
    public class MessageSend : IEmailSend, ISmsSend
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

        public async Task SendSmsAsync(string number, string message)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.twilio.com")})
            {
                client.DefaultRequestHeaders.Authorization 
                    = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String((Encoding.ASCII.GetBytes($"{Options.Sid}: {Options.AuthToken}"))));

                var contentSms = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("To", $"+{number}"),
                    new KeyValuePair<string, string>("From", "+15005550006"),
                    new KeyValuePair<string, string>("Body", message)
                });

                var results = await client.PostAsync($"/2010-04-01/Accounts/{Options.Sid}/Messages", contentSms).ConfigureAwait(false);

            }
        }
    }
}
