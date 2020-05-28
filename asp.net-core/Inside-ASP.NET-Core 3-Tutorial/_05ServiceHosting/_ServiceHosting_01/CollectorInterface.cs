using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _ServiceHosting_01
{
    public interface IProcessorMetricsCollector
    {
        int GetUsage();
    }
    public interface IMemoryMetricsCollector
    {
        long GetUsage();
    }
    public interface INetworkMetricsCollector
    {
        long GetThroughput();
    }

    public interface IMetricsDeliverer
    {
        Task DeliverAsync(PerformanceMetrics counter);
    }
}
