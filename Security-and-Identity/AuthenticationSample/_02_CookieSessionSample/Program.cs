using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace _02_CookieSessionSample
{
    class Program
    {
        static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder.UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                })
                .ConfigureLogging(loggerBuilder =>
                {
                    loggerBuilder.AddConsole();
                    loggerBuilder.AddFilter("Console", level => level >= LogLevel.Information);
                })
                .Build();

            return host.RunAsync();
        }
    }
}
