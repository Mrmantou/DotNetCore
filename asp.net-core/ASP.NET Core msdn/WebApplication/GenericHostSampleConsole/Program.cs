using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GenericHostSampleConsole
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(builder =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("hostsettings.json", optional: true);
                    builder.AddEnvironmentVariables(prefix: "PREFIX_");
                    builder.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: true);
                    builder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    builder.AddEnvironmentVariables(prefix: "PREFIX_");
                    builder.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddHostedService<LifetimeEventsHostedService>();
                    services.AddHostedService<TimedHostedService>();

                    services.AddHostedService<ConsumeScopedServiceHostedService>();
                    services.AddScoped<IScopedProcessingService, ScopedProcessingService>();

                })
                .ConfigureLogging((hostContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }

        public static async Task Main6(string[] args)
        {
            var host = new HostBuilder()
                .Build();

            Console.WriteLine("Set up a host");

            using (host)
            {
                host.Start();

                await host.WaitForShutdownAsync();
            }
        }

        public static async Task Main5(string[] args)
        {
            var host = new HostBuilder()
                .Build();

            Console.WriteLine("Set up a host");

            using (host)
            {
                host.Start();

                await host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }

        public static async Task Main4(string[] args)
        {
            var hostBuilder = new HostBuilder();

            Console.WriteLine("Set up RunConsoleAsync");

            //RunConsoleAsync enables console support, builds and starts the host, and waits for Ctrl+C/SIGINT or SIGTERM to shut down.
            await hostBuilder.RunConsoleAsync();
        }

        public static async Task Main3(string[] args)
        {
            var host = new HostBuilder()
                //.UseConsoleLifetime()
                .Build();
            Console.WriteLine("Set up a host Run Async");

            await host.RunAsync();
        }

        public static void Main2(string[] args)
        {
            var host = new HostBuilder()
                 //.UseConsoleLifetime()
                 .Build();
            Console.WriteLine("Set up a host run");

            host.Run();
        }

        public static async Task Main1(string[] args)
        {
            var host = new HostBuilder()
                //.UseConsoleLifetime()
                .Build();
            Console.WriteLine("Set up a host");

            await host.RunAsync();
        }
    }
}
