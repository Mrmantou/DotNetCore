using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostSampleConsole
{
    public class LifetimeEventsHostedService : IHostedService
    {
        private readonly ILogger<LifetimeEventsHostedService> logger;
        private readonly IApplicationLifetime appLifetime;

        public LifetimeEventsHostedService(ILogger<LifetimeEventsHostedService> logger, IApplicationLifetime appLifetime)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            logger.LogInformation("OnStarted has been called.");
        }

        private void OnStopping()
        {
            logger.LogInformation("OnStopping has been called.");
        }

        private void OnStopped()
        {
            logger.LogInformation("OnStopped has been called.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
