using System;
using System.Text;
using JobsityBot.Service.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JobsityBot.MessageBroker
{
    public class BrokerManager
    {
        private readonly IBotService _botService;
        private IConnection _connection;

        public BrokerManager(IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));

            CreateConnection();
        }

        public void Consume()
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare("commandQueue", true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var body = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var (code, chatId, roomName) = GetValues(message);
                        var result = _botService.GetStockPrice(code);
                        Console.WriteLine(result);
                        Publish(result, chatId, roomName);
                    };

                    channel.BasicConsume("commandQueue", true, consumer);
                    Console.ReadLine();
                }
            }
        }

        public void Publish(string message, string chatId, string roomName)
        {
            if(ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare("stockQueue", false, false, false, null);
                    var body = Encoding.UTF8.GetBytes($"{message} // {chatId} // {roomName}");

                    channel.BasicPublish("", "stockQueue", null, body);
                }
            }
        }

        private Tuple<string, string, string> GetValues(string message)
        {
            string[] results = message.Split(" in ");
            Console.WriteLine($"{results[0]}, {results[1]}, {results[2]}");
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
