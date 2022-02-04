using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Business.Services.Interfaces;
using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using JobsityChat.Data.Utility.Enums;

namespace JobsityChat.Business.Services
{
    public class ChatUserService : IChatUserService
    {
        private readonly IChatUserRepository _chatUserRepository;

        public ChatUserService(IChatUserRepository chatUserRepository)
        {
            _chatUserRepository = chatUserRepository ?? throw new ArgumentNullException(nameof(chatUserRepository));
        }

        public async Task JoinRoomAsync(int chatId, string userId, CancellationToken cancellationToken)
        {
            var chatUser = new ChatUser
            {
                ChatId = chatId,
                UserId = userId,
                Role = UserRole.Member
            };

            await _chatUserRepository.AddAsync(chatUser, cancellationToken);

            await _chatUserRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
