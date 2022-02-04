using Microsoft.AspNetCore.SignalR;

namespace JobsityChat.Business.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
