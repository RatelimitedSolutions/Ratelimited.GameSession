using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using Ratelimited.GameSession.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.MessageBroker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DiscordSocketClient _discordClient;
        private readonly MessageService _messageService;
        private readonly HostingService _hostingService;

        public CreateNewInstanceRequest Request { get; private set; }
        public bool IsDiscordReady = false;

        public Worker(ILogger<Worker> logger, DiscordSocketClient discord, MessageService messageService, HostingService hostingService)
        {
            _logger = logger;
            _discordClient = discord;
            _messageService = messageService;
            _hostingService = hostingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            _discordClient.Log += LogAsync;
            _discordClient.Ready += SetReady;
            await _discordClient.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
            await _discordClient.StartAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (IsDiscordReady == false)
                    continue;

                var (channel, consumer) = _messageService.ConsumNewInstanceRequests();
                consumer.Received += async (model, ea) =>
                {
                    Request = GetRequest(ea);
                    ulong GuildId = Request.GuildId;

                    Server server = _hostingService.Servers.Find(s => s.GuildId == Request.GuildId.ToString());
                    if (server == null)
                    {
                        await SendMessageToContextChannel("You can only run one session");
                        await RemoveContextMessages();
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    else
                    {
                    var session = _hostingService.CreateServer(GuildId.ToString());
                    var serverAddress = await session.CreateSessionAsync(GuildId.ToString());

                    await SendMessageToContextChannel("Server is running on: " + serverAddress);

                    await RemoveContextMessages();
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };
            }
        }

        private Task SetReady()
        {
            IsDiscordReady = true;
            return Task.Delay(0);
        }

        private async Task InitDiscordClient()
        {
            _discordClient.Log += LogAsync;
            await _discordClient.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
            await _discordClient.StartAsync();
            _discordClient.LoggedIn += LoggedIn;
        }

        private async Task SendMessageToContextChannel(string message)
        {
            var guildChannel = GetContextChannel();
            await guildChannel.SendMessageAsync(message);
        }

        private async Task RemoveContextMessages()
        {
            List<ulong> contextMessages = Request.ContextMessages;
            SocketTextChannel guildChannel;
            guildChannel = GetContextChannel();
            await guildChannel.DeleteMessagesAsync(contextMessages);
        }

        private SocketTextChannel GetContextChannel()
        {
            ulong discordChannel = Request.ChannelId;
            var guild = _discordClient.GetGuild(Request.GuildId);
            return guild.GetTextChannel(discordChannel);
        }

        private CreateNewInstanceRequest GetRequest(BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [X] Received {0}", message);

            CreateNewInstanceRequest request = JsonConvert.DeserializeObject<CreateNewInstanceRequest>(message);
            return request;
        }

        private Task LoggedIn()
        {
            _logger.LogInformation("Loggekjjid in", DateTimeOffset.Now);
            return Task.CompletedTask;

        }

        private Task LogAsync(LogMessage log)
        {
            _logger.LogInformation(log.Message, DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}
