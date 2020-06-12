using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;

namespace _Logging_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //var logger = new ServiceCollection()
            //    .AddLogging(builder => builder
            //        .AddConsole()
            //        .AddDebug())
            //    .BuildServiceProvider()
            //    .GetRequiredService<ILoggerFactory>()
            //    .CreateLogger("_Logging_01.Program");

            //var logger = new ServiceCollection()
            //    .AddLogging(builder => builder
            //        .AddConsole()
            //        .AddDebug())
            //    .BuildServiceProvider()
            //    .GetRequiredService<ILogger<Program>>();

            var listener = new FoobarEventListener();

            listener.EventSourceCreated += (sender, args) =>
            {
                if (args.EventSource.Name == "Microsoft-Extension-Logging")
                {
                    listener.EnableEvents(args.EventSource, EventLevel.LogAlways);
                }
            };

            listener.EventWritten += (sender, args) =>
            {
                if (args.EventName == "FormattedMessage")
                {
                    var payload = args.Payload;
                    var payloadNames = args.PayloadNames;
                    var indexOfLevel = payloadNames.IndexOf("Level");
                    var indexOfCategory = args.PayloadNames.IndexOf("LoggerName");
                    var indexOfEventId = args.PayloadNames.IndexOf("EventId");
                    var indexOfMessage = args.PayloadNames.IndexOf("FormattedMessage");

                    Console.WriteLine($"{(LogLevel)payload[indexOfLevel],-11}: {payload[indexOfCategory]}[{payload[indexOfEventId]}]");
                    Console.WriteLine($"{"",-13}{payload[indexOfMessage]}");
                }
            };

            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddTraceSource(
                        new SourceSwitch("default", "All"),
                        new DefaultTraceListener { LogFileName = "trace.log" })
                    .AddEventSourceLogger())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();


            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(l => l != LogLevel.None).ToArray();
            var eventId = 1;
            Array.ForEach(levels, level => logger.Log(level, eventId++, $"This is a/an {level} log message"));

            Console.WriteLine("press any to exit......");
            Console.ReadKey();
        }

        private class FoobarEventListener : EventListener { }
    }
}
