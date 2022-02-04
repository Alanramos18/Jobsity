using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using RateService.Data.Repositories;

namespace JobsityChat.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IChatDbContext context) : base(context) { }
    }
}
