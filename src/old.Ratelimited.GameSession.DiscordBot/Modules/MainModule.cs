using Discord.Commands;
using Ratelimited.GameSession.DiscordBot.Services;
using Ratelimited.GameSession.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.DiscordBot.Modules
{
    [Group("gs")]
    public class MainModule : ModuleBase<SocketCommandContext>
    {
        public HostingService _hostingService { get; set; }
        public MessageService _messageService { get; set; }

        [Command("new")]
        public async Task NewSessionAsync()
        {
            //_hostingService.CreateServer(Context.Guild.Id.ToString());
            //await Context.Channel.SendMessageAsync("Setting Up a session");
            //var ip = await _hostingService.Servers[0].CreateSessionAsync(Context.Guild.Id.ToString());
            //await Context.Channel.SendMessageAsync("Sever On: " + _hostingService.Servers[0].Adress.ToString());
            //await Context.Channel.SendMessageAsync(ip);
            var newRequest = new CreateNewInstanceRequest { ChannelId = Context.Channel.Id, GuildId = Context.Guild.Id, GuildName = Context.Guild.Name };
            
            var msg = await Context.Channel.SendMessageAsync("Server is ordered. Pls Wait");
            newRequest.ContextMessages.Add(msg.Id);
            newRequest.ContextMessages.Add(Context.Message.Id);

            _messageService.CreateNewInstance(newRequest);
        }
    }
}
