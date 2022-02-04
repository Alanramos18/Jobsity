using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobsityChat.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsityChat.Web.ViewComponents
{
    public class ChatViewComponent : ViewComponent
    {
        private IChatUserRepository _chatUserRepository;

        public ChatViewComponent(IChatUserRepository chatUserRepository)
        {
            _chatUserRepository = chatUserRepository ?? throw new ArgumentNullException(nameof(chatUserRepository));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var chats = await _chatUserRepository.Get()
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId)
                .Select(x => x.Chat)
                .ToListAsync();

            return View(chats);
        }
    }
}
