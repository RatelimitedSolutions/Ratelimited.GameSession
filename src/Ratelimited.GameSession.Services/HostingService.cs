using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QuickType;
using Renci.SshNet;
using Ratelimited.GameSession.Services;
using SshNet;
namespace Ratelimited.GameSession.Services
{
    public class HostingService
    {
        public List<Server> Servers { get; set; } = new List<Server>();
        public string ApiKey { get; set; }

        public HostingService(string apiKey)
        {
            ApiKey = apiKey;
        }

        public Server CreateServer(ulong guildId)
        {
            var newServer = new Server(guildId,ApiKey);
            Servers.Add(newServer);
            return newServer;
        }
    }

    public class Server
    {
        public string Adress { get; set; }
        public ulong GuildId { get; set; }
        private long ServerId { get; set; }
        private HttpClient httpClient;

        public Server(ulong guildId,string apiKey)
        {
            GuildId = guildId;

            httpClient = InitHttpClient(apiKey);
        }

        public async Task<string> CreateSessionAsync(ulong guildId)
        {
            var dropletResponse = await CreateDropletAsync(guildId);
            ServerId = dropletResponse.Droplet.Id;

            //var result = await GetDroplet(188591775);
            //ServerId = result.Droplet.Id;

            await WaitForSetupAsync();

            await InstallSessionOnServerAsync();
            return Adress;
        }

        public ConnectionInfo CreateConnectionInfo()
        {
            
            const string privateKeyFilePath = @"C:\Users\hoffm\Downloads\google_compute_engine";
            ConnectionInfo connectionInfo;
            using (var stream = new FileStream(privateKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                var privateKeyFile = new PrivateKeyFile(stream);
                AuthenticationMethod authenticationMethod =
                    new PrivateKeyAuthenticationMethod("root", privateKeyFile);

                connectionInfo = new ConnectionInfo(
                    Adress,
                    "root",
                    authenticationMethod);
            }

            return connectionInfo;
        }

        private async Task InstallSessionOnServerAsync()
        {
            var connectionInfo = CreateConnectionInfo();
            var result = "";
            List<string> cmds = new List<string>
            {
                "uptime",
                "adduser --disabled-password --gecos --disabled-password mcserver",
                "su - mcserver",
                "wget -O linuxgsm.sh https://linuxgsm.sh && chmod +x linuxgsm.sh && bash linuxgsm.sh mcserver",
                "exit",
                "cd",
                "./../home/mcserver/mcserver ai",
                "su - mcserver",
                "cd",
                "./mcserver ai",
                "./mcserver start",
                "sed -i '36s/.*/server-ip={Adress}/' /home/mcserver/serverfiles/server.properties",
                "./mcserver start"
            };

            using (var client = new SshClient(connectionInfo))
            {
                client.Connect();

                foreach (var cmd in cmds)
                {
                    var command = client.CreateCommand(cmd);
                    var execution =  command.BeginExecute();
                    result = command.EndExecute(execution);
                    Console.Out.WriteLine(result);
                }

                client.Disconnect();
            }
             
        }

        private async Task WaitForSetupAsync()
        {
            await Task.Delay(60000);
            var updatedDroplet = await GetDroplet(ServerId);
            while (updatedDroplet.Droplet.Networks.V4.Count == 0)
            {
                await Task.Delay(6000);
                updatedDroplet = await GetDroplet(ServerId);
            }
            Adress = updatedDroplet.Droplet.Networks.V4[0].IpAddress;
        }

        private async Task<GetASingleDroplet> GetDroplet(long id)
        {
            var response = await httpClient.GetAsync($"droplets/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();
            return QuickType.GetASingleDroplet.FromJson(responseBody);
        }

        private HttpClient InitHttpClient(string apiKey)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://api.digitalocean.com/v2/") };
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            return httpClient;
        }

        private async Task<CreateNewDroplet> CreateDropletAsync(ulong guildId)
        {
            List<long> sshKeys = await getSshKeysAsync();
            var content = new CreateDropletContent
            {
                Name = $"{guildId.ToString()}",
                Region = "fra1",
                Size = "s-1vcpu-1gb",
                Image = "ubuntu-18-04-x64",
                SshKeys = sshKeys,
                Backups = false,
                Ipv6 = false,
                UserData = null,
                PrivateNetworking= null,
                Volumes = null,
                Tags = new List<string> { "session"}
            };
            var httpContent = new StringContent(QuickType.Serialize.ToJson(content), Encoding.UTF8, "application/json");
            //httpContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = await httpClient.PostAsync("droplets", httpContent);
            string responseBody = await response.Content.ReadAsStringAsync();
            return QuickType.CreateNewDroplet.FromJson(responseBody);
        }

        private async Task<List<long>> getSshKeysAsync()
        {
            List<long> response = new List<long>();

            var webResponse = await httpClient.GetAsync("account/keys");
            string webResponseBody = await webResponse.Content.ReadAsStringAsync();
            ListAllSshKeys result = ListAllSshKeys.FromJson(webResponseBody);
            foreach (SshKey key in result.SshKeys)
            {
                response.Add(key.Id);
            }

            return response;
        }
    }
}
