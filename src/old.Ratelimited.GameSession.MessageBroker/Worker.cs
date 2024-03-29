using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        public Tuple<RabbitMQ.Client.IModel, RabbitMQ.Client.Events.BasicDeliverEventArgs> MessageServiceTulpe;
        public bool IsDiscordDone = false;
        public string resultMessage = "";

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

            while (!stoppingToken.IsCancellationRequested)
            {
                var (channel, consumer) = _messageService.ConsumNewInstanceRequests();
                consumer.Received += async (model, ea) =>
                {
                    Request = GetRequest(ea);
                    MessageServiceTulpe = new Tuple<RabbitMQ.Client.IModel, RabbitMQ.Client.Events.BasicDeliverEventArgs>(channel, ea);
                    ulong GuildId = Request.GuildId;
                    Server server = GetServer(GuildId);
                    if (server == null)
                    {
                        var session = _hostingService.CreateServer(GuildId);
                        var serverAddress = await session.CreateSessionAsync(GuildId);

                        resultMessage = "Server is running on: " + serverAddress;
                        await EndRoutine();
                    }
                    else
                    {
                        resultMessage = "You can only run one session";
                        await EndRoutine();
                    }
                };
            }
        }

        private Server GetServer(ulong guildId)
        {
            return _hostingService.Servers.Find(s => s.GuildId == guildId);
        }

        private async Task EndRoutine()
        {
            await InitDiscordClient();
        }

        private void CheckoutToChannel(RabbitMQ.Client.IModel channel, RabbitMQ.Client.Events.BasicDeliverEventArgs ea)
        {
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            Console.WriteLine("DONE");
        }

        private async Task OnReady()
        {
            await RemoveContextMessages();
            await SendMessageToContextChannel(resultMessage);
            CheckoutToChannel(MessageServiceTulpe.Item1, MessageServiceTulpe.Item2);
        }

        private async Task InitDiscordClient()
        {
            _discordClient.Log += LogAsync;
            _discordClient.Ready += OnReady;
            await _discordClient.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
            await _discordClient.StartAsync();
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

        private CreateNewInstanceRequest GetRequest(RabbitMQ.Client.Events.BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [X] Received {0}", message);

            CreateNewInstanceRequest request = JsonConvert.DeserializeObject<CreateNewInstanceRequest>(message);
            return request;
        }

        private Task LogAsync(LogMessage log)
        {
            _logger.LogInformation(log.Message, DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}
