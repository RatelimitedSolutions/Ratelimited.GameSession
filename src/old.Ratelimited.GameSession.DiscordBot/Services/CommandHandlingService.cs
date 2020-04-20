using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.DiscordBot.Services
{
    public class CommandHandlingService
    {
        private CommandService _commands;
        private DiscordSocketClient _discord;
        private IServiceProvider _services;

        public CommandHandlingService(IServiceProvider services)
        {
            _commands = services.GetRequiredService<CommandService>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            _commands.CommandExecuted += CommandExecutedAsync;

            _discord.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task MessageReceivedAsync(SocketMessage socketMessage)
        {
            if(!(socketMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            int argPossition = 0;
            if (!message.HasCharPrefix('!', ref argPossition)) return;

            SocketCommandContext context = new SocketCommandContext(_discord, message);

            await _commands.ExecuteAsync(context, argPossition, _services);
        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // Not found
            if (!command.IsSpecified)
                return;

            if (result.IsSuccess)
                return;

            await context.Channel.SendMessageAsync($"Error: {result}");
        }
    }
}
