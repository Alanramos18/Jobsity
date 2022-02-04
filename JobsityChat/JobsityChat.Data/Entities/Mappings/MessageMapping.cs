using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsityChat.Data.Entities.Mappings
{
    internal class MessageMapping : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("MESSAGES");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(t => t.Text)
                .HasColumnName("text")
                .IsRequired();

            builder.Property(t => t.Timestamp)
                .HasColumnName("stamp")
                .IsRequired();

            builder.HasOne(y => y.Chat)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatId);
        }
    }
}
