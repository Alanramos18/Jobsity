using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Business.Hubs;
using JobsityChat.Business.Services.Interfaces;
using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using JobsityChat.Data.Utility.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace JobsityChat.Business.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IHubContext<ChatHub> _chat;

        public ChatService(IChatRepository chatRepository, IHubContext<ChatHub> chat)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _chat = chat ?? throw new ArgumentNullException(nameof(chat));
        }

        public async Task<List<Chat>> GetRoomsForIndexAsync(string userId, CancellationToken cancellationToken)
        {
            var rooms = await _chatRepository.GetUserRoomsAsync(userId, cancellationToken);

            return rooms;
        }

        public async Task<Chat> GetRoomAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _chatRepository.GetByIdAsync(id, cancellationToken);

            return room;
        }

        public async Task CreateRoomAsync(string name, string userId, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser
            {
                UserId = userId,
                Role = UserRole.Admin
            });

            await _chatRepository.AddAsync(chat, cancellationToken);

            await _chatRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task JoinRoomAsync(string connectionId, string roomName, CancellationToken cancellationToken)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName, cancellationToken);
        }

        public async Task LeaveRoomAsync(string connectionId, string roomName, CancellationToken cancellationToken)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName, cancellationToken);
        }
    }
}
