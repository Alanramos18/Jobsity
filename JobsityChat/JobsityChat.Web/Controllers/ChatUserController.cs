using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsityChat.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ChatUserController : Controller
    {
        private readonly IChatUserService _chatUserService;
        public ChatUserController(IChatUserService chatUserService)
        {
            _chatUserService = chatUserService ?? throw new ArgumentNullException(nameof(chatUserService));
        }

        [HttpGet]
        [Route("JoinRoomAsync/{id}")]
        public async Task<IActionResult> JoinRoomAsync(int id, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _chatUserService.JoinRoomAsync(id, userId, cancellationToken);
            
            return RedirectToAction("GetRoomAsync", "Chat", new { id = id });
        }
    }
}
