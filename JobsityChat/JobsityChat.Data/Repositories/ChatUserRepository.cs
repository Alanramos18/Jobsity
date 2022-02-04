using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using RateService.Data.Repositories;

namespace JobsityChat.Data.Repositories
{
    public class ChatUserRepository : BaseRepository<ChatUser>, IChatUserRepository
    {
        public ChatUserRepository(IChatDbContext context) : base(context) { }
    }
}
