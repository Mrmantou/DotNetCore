using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

namespace _Start_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder => builder
                    .UseKestrel()
                    .Configure(app => app
                        .Run(context => context.Response.WriteAsync("Hello World"))))
                .Build()
                .Run();
        }
    }
}
