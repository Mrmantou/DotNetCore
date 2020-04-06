using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _ServiceHosting_02
{
    public class FakeMetricsCollector :
        IProcessorMetricsCollector,
        IMemoryMetricsCollector,
        INetworkMetricsCollector
    {
        long INetworkMetricsCollector.GetThroughput() => PerformanceMetrics.Create().Network;

        int IProcessorMetricsCollector.GetUsage() => PerformanceMetrics.Create().Processor;

        long IMemoryMetricsCollector.GetUsage() => PerformanceMetrics.Create().Memory;
    }

    public class FakeMetricsDeliverer : IMetricsDeliverer
    {
        private readonly TransportType transport;
        private readonly Endpoint deliverTo;

        public FakeMetricsDeliverer(IOptions<MetricsCollectionOptions> optionsAccessor)
        {
            var options = optionsAccessor.Value;
            transport = options.Transport;
            deliverTo = options.DeliverTo;
        }

        public Task DeliverAsync(PerformanceMetrics counter)
        {
            Console.WriteLine($"[{DateTimeOffset.UtcNow}] Deliver performance counter {counter} to {deliverTo} via {transport}");
            return Task.CompletedTask;
        }
    }
}
