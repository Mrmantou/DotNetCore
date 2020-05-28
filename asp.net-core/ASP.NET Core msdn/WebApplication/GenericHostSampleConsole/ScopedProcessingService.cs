using Microsoft.Extensions.Logging;

namespace GenericHostSampleConsole
{
    internal interface IScopedProcessingService
    {
        void DoWork();
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger logger;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            this.logger = logger;
        }

        public void DoWork()
        {
            logger.LogInformation("Scoped Processing Service is working.");
        }
    }
}
