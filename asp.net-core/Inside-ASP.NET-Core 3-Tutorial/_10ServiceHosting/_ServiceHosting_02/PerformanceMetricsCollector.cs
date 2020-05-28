using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _ServiceHosting_02
{
    public class PerformanceMetricsCollector : IHostedService
    {
        private readonly IProcessorMetricsCollector processorMetricsCollector;
        private readonly IMemoryMetricsCollector memoryMetricsCollector;
        private readonly INetworkMetricsCollector networkMetricsCollector;
        private readonly IMetricsDeliverer metricsDeliverer;
        private readonly TimeSpan captureInterval;
        private IDisposable scheduler;

        public PerformanceMetricsCollector(
            IProcessorMetricsCollector processorMetricsCollector,
            IMemoryMetricsCollector memoryMetricsCollector,
            INetworkMetricsCollector networkMetricsCollector,
            IMetricsDeliverer metricsDeliverer,
            IOptions<MetricsCollectionOptions> optionsAccessor)
        {
            this.processorMetricsCollector = processorMetricsCollector;
            this.memoryMetricsCollector = memoryMetricsCollector;
            this.networkMetricsCollector = networkMetricsCollector;
            this.metricsDeliverer = metricsDeliverer;
            captureInterval = optionsAccessor.Value.CaptureInterval;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), captureInterval);
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
