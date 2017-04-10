using Microsoft.Extensions.Options;
using System;
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
            throw new NotImplementedException();
        }
    }
}
