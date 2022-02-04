using System.Threading;
using System.Threading.Tasks;

namespace JobsityChat.Business.Services.Interfaces
{
    public interface IChatUserService
    {
        Task JoinRoomAsync(int chatId, string userId, CancellationToken cancellationToken);
    }
}
