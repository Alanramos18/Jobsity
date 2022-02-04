using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Data.Entities;

namespace JobsityChat.Business.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<Chat>> GetRoomsForIndexAsync(string userId, CancellationToken cancellationToken);
        
        Task<Chat> GetRoomAsync(int id, CancellationToken cancellationToken);

        Task CreateRoomAsync(string name, string userId, CancellationToken cancellationToken);

        Task JoinRoomAsync(string connectionId, string roomName, CancellationToken cancellationToken);

        Task LeaveRoomAsync(string connectionId, string roomName, CancellationToken cancellationToken);
    }
}
