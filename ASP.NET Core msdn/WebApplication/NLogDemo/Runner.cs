using Microsoft.Extensions.Logging;

namespace NLogDemo
{
    public class Runner
    {
        private readonly ILogger<Runner> logger;

        public Runner(ILogger<Runner> logger)
        {
            this.logger = logger;
        }

        public void DoAction(string name)
        {
            logger.LogDebug(20, "Doing hard work! {Action}", name);
            logger.LogInformation(20, "Doing hard work! {Action}", name);
        }
    }
}
