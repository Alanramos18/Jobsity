using System;
using JobsityBot.MessageBroker;
using JobsityBot.Service;
using JobsityBot.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobsityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<IFileReader, FileReader>()
            .AddTransient<IBotService, BotService>()
            .BuildServiceProvider();

            var consume = new BrokerManager(serviceProvider.GetRequiredService<IBotService>());
            consume.Consume();

            Console.ReadLine();
        }
    }
}
