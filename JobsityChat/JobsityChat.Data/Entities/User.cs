using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace JobsityChat.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<ChatUser> Chats { get; set; }
    }
}
