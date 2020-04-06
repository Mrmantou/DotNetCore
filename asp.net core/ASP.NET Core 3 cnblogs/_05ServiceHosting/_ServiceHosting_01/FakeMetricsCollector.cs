using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _ServiceHosting_01
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
        public Task DeliverAsync(PerformanceMetrics counter)
        {
            Console.WriteLine($"[{DateTimeOffset.UtcNow}] {counter}");
            return Task.CompletedTask;
        }
    }
}
