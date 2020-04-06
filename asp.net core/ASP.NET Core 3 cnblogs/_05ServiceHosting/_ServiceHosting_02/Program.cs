using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _ServiceHosting_02
{
    class Program
    {
        /*
         * dotnet run         //default: production
         * dotnet run /environment=development
         * dotnet run /environment=staging
         * dotnet run /environment=production
         */
        static void Main(string[] args)
        {
            var collector = new FakeMetricsCollector();
            new HostBuilder()
                .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
                .ConfigureAppConfiguration((context, builder) => builder
                    .AddJsonFile(path: "appsettings.json", optional: false)
                    .AddJsonFile(path: $"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true))
                .ConfigureServices((context, services) => services
                    .AddSingleton<IProcessorMetricsCollector>(collector)
                    .AddSingleton<IMemoryMetricsCollector>(collector)
                    .AddSingleton<INetworkMetricsCollector>(collector)
                    .AddSingleton<IMetricsDeliverer, FakeMetricsDeliverer>()
                    .AddSingleton<IHostedService, PerformanceMetricsCollector>()
                    .AddOptions()
                    .Configure<MetricsCollectionOptions>(context.Configuration.GetSection("MetricsCollection")))
                .Build()
                .Run();
        }
    }
}
