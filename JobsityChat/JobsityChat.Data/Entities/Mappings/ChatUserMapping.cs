using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsityChat.Data.Entities.Mappings
{
    internal class ChatUserMapping : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.ToTable("CHATUSER");
            builder.HasKey(t => new { t.ChatId, t.UserId });

            builder.Property(t => t.ChatId)
                .HasColumnName("chatId")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.UserId)
                .HasColumnName("userId")
                .IsRequired();

            builder.HasOne(y => y.Chat)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ChatId);

            builder.HasOne(y => y.User)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.UserId);
        }
    }
}
