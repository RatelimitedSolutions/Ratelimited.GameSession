using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ratelimited.GameSession.Services;
using System;

namespace Ratelimited.GameSession.MessageBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<DiscordSocketClient>();
                        services.AddSingleton<HostingService>(provider => new HostingService(Environment.GetEnvironmentVariable("API")));
                        services.AddSingleton<MessageService>(provider => new MessageService(Environment.GetEnvironmentVariable("RABBIT")));
                        services.AddHostedService<Worker>();
                    });
        }
    }
}
