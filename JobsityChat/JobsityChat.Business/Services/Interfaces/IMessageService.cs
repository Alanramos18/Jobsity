using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Data.Entities;

namespace JobsityChat.Business.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(int chatId, string message, string roomName, string userName, CancellationToken cancellationToken);
        Task BotPostAsync(int chatId, string message, string roomName, CancellationToken cancellationToken);
    }
}
