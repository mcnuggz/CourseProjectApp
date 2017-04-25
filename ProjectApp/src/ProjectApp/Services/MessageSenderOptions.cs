namespace ProjectApp.Services
{
    public class MessageSenderOptions
    {
        public string SendGridApiKey { get; set; }

        //change to Live Credentials when ready to publish
        public string Sid { get; set; }

        public string AuthToken { get; set; }
    }
}
