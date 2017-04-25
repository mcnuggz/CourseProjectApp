using System.Threading.Tasks;

namespace ProjectApp.Services
{
    public interface ISmsSend
    {
        Task SendSmsAsync(string number, string message);
    }
}
