using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JobsityChat.Business.MessageBroker
{
    public class ConsumeRabbit : BackgroundService
    {
        private IMessageService _messageService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private string _message = "";
        private string _chatId = "";
        private string _roomName = "";

        public ConsumeRabbit(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);


                await Task.Delay(100, cancellationToken);
            }

        }

        public (string, string, string) Consume()
        {
            if (ConnectionExists())
            {
                _channel.BasicConsume("stockQueue", true, _consumer);
            }

            return (_message, _chatId, _roomName);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _messageService = scope.ServiceProvider.GetService<IMessageService>();

                cancellationToken.ThrowIfCancellationRequested();

                var (message, chatId, roomName) = Consume();

                if (!String.IsNullOrEmpty(message))
                {
                    await _messageService.BotPostAsync(Int32.Parse(chatId), message, roomName, cancellationToken);
                    _message = "";
                }
            }
        }

        private Tuple<string, string, string> GetValues(string message)
        {
            string[] results = message.Split(" // ");
            return new Tuple<string, string, string>(results[0], results[1], results[2]);
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost"
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare("stockQueue", false, false, false, null);

                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    (_message, _chatId, _roomName) = GetValues(message);

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }

    }
}
