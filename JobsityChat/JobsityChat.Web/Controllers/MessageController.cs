using System;
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
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SendMessage([FromForm] int chatId, [FromForm] string message, [FromForm] string roomName, CancellationToken cancellationToken = default)
        {
            var user = User.Identity.Name;

            await _messageService.SendMessage(chatId, message, roomName, user, cancellationToken);

            return Ok();
        }

    }
}
