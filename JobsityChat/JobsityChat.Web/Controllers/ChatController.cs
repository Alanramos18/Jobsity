using System;
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
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(await _chatService.GetRoomsForIndexAsync(userId, cancellationToken));
        }

        [HttpGet]
        [Route("GetRoomAsync/{id}")]
        public async Task<IActionResult> GetRoomAsync(int id, CancellationToken cancellationToken = default)
        {
            var room = await _chatService.GetRoomAsync(id, cancellationToken);

            return View("Chat", room);
        }

        [HttpPost]
        [Route("CreateRoomAsync")]
        public async Task<IActionResult> CreateRoomAsync([FromForm]string name, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _chatService.CreateRoomAsync(name, userId, cancellationToken);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName, CancellationToken cancellationToken = default)
        {
            await _chatService.JoinRoomAsync(connectionId, roomName, cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Route("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveChat(string connectionId, string roomName, CancellationToken cancellationToken = default)
        {
            await _chatService.LeaveRoomAsync(connectionId, roomName, cancellationToken);

            return Ok();
        }
    }
}
