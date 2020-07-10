using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace _PipeLine_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureLogging(options=>options.ClearProviders())
                    .UseStartup<Startup>()
                    .UseSetting("environment", "Staging")
                    .UseSetting("contentRoot", Path.Combine(Directory.GetCurrentDirectory(), "contents"))
                    .UseSetting("webroot", Path.Combine(Directory.GetCurrentDirectory(), "contents/web"))
                    .UseSetting("applicationName", "WebApp"))
                .Build()
                .Run();
        }
    }

    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            Console.WriteLine(environment.ApplicationName);
            Console.WriteLine(environment.EnvironmentName);
            Console.WriteLine(environment.ContentRootPath);
            Console.WriteLine(environment.WebRootPath);
        }

        public void Configure(IApplicationBuilder app) { }
    }
}
