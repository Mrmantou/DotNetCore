using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ObjectGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var config = builder.Build();

            var appConfig = new AppSettings();

            //  config.GetSection("App").Bind(appConfig);
            appConfig = config.GetSection("App").Get<AppSettings>();

            Console.WriteLine($"Height {appConfig.Window.Height}");
            Console.WriteLine();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
