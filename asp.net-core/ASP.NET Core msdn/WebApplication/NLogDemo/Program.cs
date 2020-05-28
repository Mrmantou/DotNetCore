using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace NLogDemo
{
    /// <summary>
    /// NLog for .net core consoleApp
    /// https://github.com/NLog/NLog.Extensions.Logging/wiki/Getting-started-with-.NET-Core-2---Console-application
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var servicepProvider = BuildDI();

            var runner = servicepProvider.GetRequiredService<Runner>();

            runner.DoAction("hello ");

            runner.DoAction("world!");
            int i = 0;
            while (i++ < 1000)
            {
                runner.DoAction("demo");
            }

            Console.WriteLine("press any key to exit...");
            Console.ReadKey();

            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }

        private static IServiceProvider BuildDI()
        {
            var services = new ServiceCollection();

            services.AddTransient<Runner>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddLogging(builder => { builder.SetMinimumLevel(LogLevel.Trace); });

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddNLog();

            /**
             * 教程使用下面的代码进行配置
             * 这里使用上面的进行配置是因为直接引用了 NLog.Config 自动进行配置文件的设置
             * **/
            //configure NLog
            //loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            //NLog.LogManager.LoadConfiguration("nlog.config");

            return serviceProvider;
        }
    }
}
