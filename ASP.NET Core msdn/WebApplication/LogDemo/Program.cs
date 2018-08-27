using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LogDemo
{
    class Program
    {
        private readonly ILogger logger;

        public Program()
        {
            var loggingConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder
                        .AddConfiguration(loggingConfiguration.GetSection("Logging"))
                        .AddConsole();

                });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        }

        static void Main(string[] args)
        {
            new Program().Execute(args);

            Console.WriteLine("Hello World!");
        }

        private void Execute(string[] args)
        {
            logger.LogInformation("Starting......");

            var startTime = DateTimeOffset.Now;

            logger.LogInformation(1, "Started at '{StartTime}' and 0x{Hello:X} is hex of 42", startTime, 42);

            logger.ProgramStarting(startTime, 42);

            using (logger.PurchaseOrderScope("00655321"))
            {
                try
                {
                    throw new Exception("Boom");
                }
                catch (Exception ex)
                {
                    logger.LogCritical(1, ex, "Unexpected critical error startingapplication");
                    logger.LogError(1, ex, "Unexpected error");
                    logger.LogWarning(1, ex, "Unexpected warning");
                }

                using (logger.BeginScope("Main"))
                {
                    logger.LogInformation("Waiting for user input");

                    string input = string.Empty;

                    do
                    {
                        Console.WriteLine("Enter some test to log more, or 'quit' to exit.");
                        input = Console.ReadLine();

                        logger.LogInformation("user typed '{input}' on the command line", input);
                        logger.LogWarning("The time is now {Time}, it's getting late!", DateTimeOffset.Now);
                    }
                    while (input != "quit");
                }
            }

            var endTime = DateTimeOffset.Now;
            logger.LogInformation(2, "Stopping at '{StopTime}'", endTime);
            // or
            logger.ProgramStopping(endTime);

            logger.LogInformation("Stopping");
        }
    }
}
