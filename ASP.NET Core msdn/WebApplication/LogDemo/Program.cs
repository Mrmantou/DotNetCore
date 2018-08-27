using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

namespace LogDemo
{
    class Program
    {
        private readonly ILogger logger;

        /// <summary>
        /// Description:主程序构造方法
        /// <para>-----:通过ConfigurationBuilder获取配置文件中的logger配置</para>
        /// <para>-----:通过依赖注入创建logger对象</para>
        /// <para>-----:</para>
        /// </summary>
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
                        .AddDebug()
                        .AddConsole();
                });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        }

        static void Main(string[] args)
        {
            //new Program().Execute(args);
            new Program().SampleDemo();

            Thread.Sleep(500);

            Console.WriteLine("Hello World!");
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
            
        }

        /* 配置文件
        {
          "Logging": {
            "LogLevel": {
              "Default": "Debug", 设置日志显示级别
              "System": "Information",
              "Microsoft": "Information"
            },
            "Console": {
              "IncludeScopes": "false" 设置是否按照LogLevel进行过滤
            }
          }
        }
        */
        private void SampleDemo()
        {
            //日志等级 enum LogLevel
            logger.LogTrace("Hello world! -- trace");
            logger.LogDebug("Hello world! -- debug");
            logger.LogInformation("Hello world! -- info");
            logger.LogWarning("Hello world! -- warning");
            logger.LogError("Hello world! -- error");
            logger.LogCritical("Hello world! -- critical");
            
            logger.LogInformation(100, "Hello world! -- info");
            logger.LogInformation(101, "Hello world! -- info");

            logger.LogInformation(102, "Hello world! {1} -- info",123);

            try
            {
                throw new Exception("exception test");
            }
            catch(Exception ex)
            {
                logger.LogWarning(103, ex, "a exception {ex}", "try throw");
            }
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
