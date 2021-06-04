using App.Diagnostics;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace App.Tasks
{
    public class DiagnosticTask : IHostedService
    {
        private readonly TracingDiagnosticProcessorObserver tracingDiagnosticProcessorObserver;
        public DiagnosticTask(TracingDiagnosticProcessorObserver tracingDiagnosticProcessorObserver)
        {
            this.tracingDiagnosticProcessorObserver = tracingDiagnosticProcessorObserver;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DiagnosticListener.AllListeners.Subscribe(tracingDiagnosticProcessorObserver);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"stop {nameof(DiagnosticTask)}");
            return Task.CompletedTask;
        }
    }
}
