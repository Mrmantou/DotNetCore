using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace _PipeLine_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureLogging(builder => builder
                    .AddConsole(options => options.IncludeScopes = true))
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app.Run(context =>
                    {
                        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogInformation("Log for event Foobar");

                        if (context.Request.Path == new PathString("/error"))
                        {
                            throw new InvalidOperationException("Manually, throw exception.");
                        }

                        return Task.CompletedTask;
                    })))
                .Build()
                .Run();
        }
    }
}
