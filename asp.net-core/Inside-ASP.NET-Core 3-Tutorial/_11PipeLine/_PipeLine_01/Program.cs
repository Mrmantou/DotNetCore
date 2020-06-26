using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

namespace _PipeLine_01
{
    class Program
    {
        static void Main(string[] args)
        {
            static RequestDelegate Middleware1(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync("Hello");
                await next(context);
            };

            static RequestDelegate Middleware2(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync(" World!");
            };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app
                        .Use(Middleware1)
                        .Use(Middleware2)))
                .Build()
                .Run();




            //----------------------------------------------------------------
            //Host.CreateDefaultBuilder()
            //    .ConfigureWebHostDefaults(builder => builder
            //        .Configure(app => app.Run(context => 
            //            context.Response.WriteAsync("Hello World!"))))
            //    .Build()
            //    .Run();
        }
    }
}
