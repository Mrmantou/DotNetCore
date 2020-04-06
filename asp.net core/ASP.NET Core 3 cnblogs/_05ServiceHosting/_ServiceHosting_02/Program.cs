using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _ServiceHosting_02
{
    class Program
    {
        static void Main(string[] args)
        {
            var collector = new FakeMetricsCollector();
            new HostBuilder()
                .ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json"))
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
