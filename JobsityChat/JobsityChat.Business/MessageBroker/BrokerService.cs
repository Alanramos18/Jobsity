using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JobsityChat.Business.MessageBroker
{
    public class BrokerService : IBrokerService
    {
        private IConnection _connection;
        public BrokerService()
        {
            CreateConnection();
        }

        public void Publish(string message, string chatId, string roomName)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("commandQueue", true, false, false, null);

                    var body = Encoding.UTF8.GetBytes($"{message} in {chatId} in {roomName}");

                    channel.BasicPublish("", "commandQueue", null, body);
                }
            }
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
