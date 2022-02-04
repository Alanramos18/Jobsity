using System;
using System.Collections.Generic;
using System.Text;
using JobsityChat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsityChat.Data
{
    public interface IChatDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
    }
}
