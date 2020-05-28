using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

namespace _Start_02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Host.CreateDefaultBuilder()
            //    .ConfigureWebHost(builder => builder
            //        .UseKestrel()
            //        .UseUrls("http://0.0.0.0:6100;https://0.0.0.0:6101")
            //        .Configure(app => app
            //            .Run(context => context.Response.WriteAsync("Hello World"))))
            //    .Build()
            //    .Run();

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app
                        .Run(context => context.Response.WriteAsync("Hello World"))))
                .Build()
                .Run();
        }
    }
}
