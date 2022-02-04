using System.Collections.Generic;
using JobsityChat.Data.Utility.Enums;

namespace JobsityChat.Data.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ChatType Type { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public ICollection<ChatUser> Users { get; set; } = new List<ChatUser>();
    }
}
