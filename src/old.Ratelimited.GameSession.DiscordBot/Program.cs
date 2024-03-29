﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Ratelimited.GameSession.DiscordBot.Services;
using Ratelimited.GameSession.Services;

namespace Ratelimited.GameSession.DiscordBot
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                // Discord Bot  Init
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
                await client.StartAsync();

                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(-1);
            }
        }


        private Task LogAsync(LogMessage log)
        {
            var date = new DateTime();
            Console.WriteLine($"[{date.ToLocalTime()}] {log.ToString()}");

            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<HostingService>(provider => new HostingService(Environment.GetEnvironmentVariable("API")))
                .AddSingleton<MessageService>(provider => new MessageService(Environment.GetEnvironmentVariable("RABBIT")))
                .BuildServiceProvider();
        }

    }
}
