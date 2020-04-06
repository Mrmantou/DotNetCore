using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace _ServiceHosting_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //new HostBuilder()
            //    .ConfigureServices(services =>
            //        //services.AddSingleton<IHostedService, PerformanceMetricsCollector>())
            //        services.AddHostedService<PerformanceMetricsCollector>())
            //    .Build()
            //    .Run();


            var collector = new FakeMetricsCollector();
            new HostBuilder()
                .ConfigureServices(services => services
                    .AddSingleton<IProcessorMetricsCollector>(collector)
                    .AddSingleton<IMemoryMetricsCollector>(collector)
                    .AddSingleton<INetworkMetricsCollector>(collector)
                    .AddSingleton<IMetricsDeliverer, FakeMetricsDeliverer>()
                    .AddSingleton<IHostedService, PerformanceMetricsNewCollector>())
                .Build()
                .Run();


        }
    }
}
