using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FakeMetricsDeliverer> logger;
        private readonly Action<ILogger, DateTimeOffset, PerformanceMetrics, Endpoint, TransportType, Exception> logForDelivery;

        public FakeMetricsDeliverer(IOptions<MetricsCollectionOptions> optionsAccessor, ILogger<FakeMetricsDeliverer> logger)
        {
            var options = optionsAccessor.Value;
            transport = options.Transport;
            deliverTo = options.DeliverTo;
            this.logger = logger;
            logForDelivery = LoggerMessage.Define<DateTimeOffset, PerformanceMetrics, Endpoint, TransportType>(LogLevel.Information, 0, "[{0}]Deliver performance counter {1} to {2} via {3}");
        }

        public Task DeliverAsync(PerformanceMetrics counter)
        {
            logForDelivery(logger, DateTimeOffset.UtcNow, counter, deliverTo, transport, null);
            //Console.WriteLine($"[{DateTimeOffset.UtcNow}] Deliver performance counter {counter} to {deliverTo} via {transport}");
            return Task.CompletedTask;
        }
    }
}
