using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _ServiceHosting_01
{
    public sealed class PerformanceMetricsCollector : IHostedService
    {
        private IDisposable scheduler;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

            return Task.CompletedTask;

            static void Callback(object state)
            {
                Console.WriteLine($"[{DateTimeOffset.Now}] {PerformanceMetrics.Create()}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }
}
