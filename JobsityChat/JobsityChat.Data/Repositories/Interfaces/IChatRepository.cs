using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Data.Entities;

namespace JobsityChat.Data.Repositories.Interfaces
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<List<Chat>> GetUserRoomsAsync(string userId, CancellationToken cancellationToken);
    }
}
