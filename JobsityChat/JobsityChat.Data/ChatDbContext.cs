using JobsityChat.Data.Entities;
using JobsityChat.Data.Entities.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobsityChat.Data
{
    public class ChatDbContext : IdentityDbContext<User>, IChatDbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ChatMapping());
            builder.ApplyConfiguration(new MessageMapping());
            builder.ApplyConfiguration(new ChatUserMapping());
        }
    }
}
