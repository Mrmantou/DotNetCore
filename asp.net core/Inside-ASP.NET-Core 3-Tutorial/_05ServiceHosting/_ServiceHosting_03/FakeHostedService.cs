using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _ServiceHosting_03
{
    public class FakeHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime lifetime;
        private IDisposable tokenSource;

        public FakeHostedService(IHostApplicationLifetime lifetime)
        {
            this.lifetime = lifetime;
            this.lifetime.ApplicationStarted.Register(() => Console.WriteLine($"[{DateTimeOffset.Now}] Application started"));
            this.lifetime.ApplicationStopping.Register(() => Console.WriteLine($"[{DateTimeOffset.Now}] Application is stopping"));
            this.lifetime.ApplicationStopped.Register(() => Console.WriteLine($"[{DateTimeOffset.Now}] Application stoped"));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token.Register(lifetime.StopApplication);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            tokenSource.Dispose();
            return Task.CompletedTask;
        }
    }
}
