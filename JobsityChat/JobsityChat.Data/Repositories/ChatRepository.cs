using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using RateService.Data.Repositories;

namespace JobsityChat.Data.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(IChatDbContext context) : base(context) { }

        public override Task<Chat> GetByIdAsync(int entityId, CancellationToken cancellationToken)
        {
            return _context.Chats
                .Include(x => x.Messages.OrderBy(y => y.Timestamp).Take(50))
                .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
        }

        public Task<List<Chat>> GetUserRoomsAsync(string userId, CancellationToken cancellationToken)
        {
            return _context.Chats
                 .Include(x => x.Users)
                 .Where(x => !x.Users.Any(y => y.UserId == userId))
                 .ToListAsync(cancellationToken);
        }
    }
}
