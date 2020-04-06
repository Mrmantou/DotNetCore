using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _ServiceHosting_01
{
    public class PerformanceMetricsNewCollector : IHostedService
    {
        private readonly IProcessorMetricsCollector processorMetricsCollector;
        private readonly IMemoryMetricsCollector memoryMetricsCollector;
        private readonly INetworkMetricsCollector networkMetricsCollector;
        private readonly IMetricsDeliverer metricsDeliverer;
        private IDisposable scheduler;

        public PerformanceMetricsNewCollector(
            IProcessorMetricsCollector processorMetricsCollector,
            IMemoryMetricsCollector memoryMetricsCollector,
            INetworkMetricsCollector networkMetricsCollector,
            IMetricsDeliverer metricsDeliverer)
        {
            this.processorMetricsCollector = processorMetricsCollector;
            this.memoryMetricsCollector = memoryMetricsCollector;
            this.networkMetricsCollector = networkMetricsCollector;
            this.metricsDeliverer = metricsDeliverer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;

            async void Callback(object state)
            {
                var counter = new PerformanceMetrics
                {
                    Processor = processorMetricsCollector.GetUsage(),
                    Memory = memoryMetricsCollector.GetUsage(),
                    Network = networkMetricsCollector.GetThroughput()
                };
                await metricsDeliverer.DeliverAsync(counter);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }
}
