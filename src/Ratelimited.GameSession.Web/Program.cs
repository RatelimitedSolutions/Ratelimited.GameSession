using DotNetCore.AspNetCore;
using DotNetCore.Logging;
using Microsoft.Extensions.Hosting;


namespace Ratelimited.GameSession.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder().UseSerilog().Run<Startup>();
        }
    }
}
