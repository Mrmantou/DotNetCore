using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;

namespace _Logging_02
{
    class Program
    {
        static void Main(string[] args)
        {
            //var logger = new ServiceCollection()
            //    .AddLogging(builder => builder
            //        .SetMinimumLevel(LogLevel.Trace)
            //        .AddConsole())
            //    .BuildServiceProvider()
            //    .GetRequiredService<ILogger<Program>>();

            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddFilter(Filter)
                    .AddConsole())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(l => l != LogLevel.None).ToArray();
            var eventId = 1;
            Array.ForEach(levels, level => logger.Log(level, eventId++, $"This is a/an {level} log message"));

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();

            static bool Filter(string category, LogLevel level)
            {
                return category switch
                {
                    "Foo" => level >= LogLevel.Debug,
                    "Bar" => level >= LogLevel.Warning,
                    "Baz" => level >= LogLevel.None,
                    _ => level >= LogLevel.Information
                };
            }
        }

        static void SetLevel()
        {
            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(l => l != LogLevel.None).ToArray();
            var eventId = 1;
            Array.ForEach(levels, level => logger.Log(level, eventId++, $"This is a/an {level} log message"));
        }
    }
}
