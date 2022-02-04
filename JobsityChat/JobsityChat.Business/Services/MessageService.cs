using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Business.Hubs;
using JobsityChat.Business.MessageBroker;
using JobsityChat.Business.Services.Interfaces;
using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace JobsityChat.Business.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        private IBrokerService _brokerService;
        private readonly IHubContext<ChatHub> _chat;
        private string _code;

        public MessageService(IMessageRepository messageRepository, IHubContext<ChatHub> chat, IBrokerService brokerService)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _chat = chat ?? throw new ArgumentNullException(nameof(chat));
            _brokerService = brokerService ?? throw new ArgumentNullException(nameof(brokerService));
        }

        public async Task SendMessage(int chatId, string message, string roomName, string userName, CancellationToken cancellationToken)
        {
            if(ValidateCommand(message))
            {
                _brokerService.Publish(_code, chatId.ToString(), roomName);

                await _chat.Clients.Group(roomName)
                .SendAsync("RecieveMessage", new
                {
                    Text = message,
                    Name = userName,
                    Timestamp = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")
                });

                return;
            }

            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = userName,
                Timestamp = DateTime.Now
            };

            await _messageRepository.AddAsync(msg, cancellationToken);

            await _messageRepository.SaveChangesAsync(cancellationToken);

            await _chat.Clients.Group(roomName)
                .SendAsync("RecieveMessage", new
                {
                    Text = msg.Text,
                    Name = msg.Name,
                    Timestamp = msg.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });
        }

        public async Task BotPostAsync(int chatId, string message, string roomName, CancellationToken cancellationToken)
        {
            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = "Bot",
                Timestamp = DateTime.Now
            };

            await _chat.Clients.Group(roomName)
                .SendAsync("RecieveMessage", new
                {
                    Text = msg.Text,
                    Name = msg.Name,
                    Timestamp = msg.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });
        }

        private bool ValidateCommand(string text)
        {
            StringBuilder temp = new StringBuilder();
            var i = 0;
            var lenght = text.Length - 1;
            if(text[i] == '/')
            {
                i++;
                while(i <= lenght && text[i] != '=')
                {
                    temp.Append(text[i]);
                    i++;
                }

                if (i > lenght)
                    return false;

                if(temp.ToString().Equals("stock"))
                {
                    temp.Clear();
                    i++;
                    while (i <= lenght)
                    {
                        temp.Append(text[i]);
                        i++;
                    }

                    _code = temp.ToString();
                    return true;
                }
            }

            return false;
        }
    }
}
