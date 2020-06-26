using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace _PipeLine_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app
                        .UseMiddleware<StringContentMiddlewareAppointment>("Hello")
                        .UseMiddleware<StringContentMiddlewareAppointment>(" World!", false)))
                .Build()
                .Run();

            //Host.CreateDefaultBuilder()
            //    .ConfigureWebHostDefaults(builder => builder
            //        .ConfigureServices(services => services
            //            .AddSingleton(new StringContentMiddleware("Hello World!")))
            //        .Configure(app => app
            //            .UseMiddleware<StringContentMiddleware>()))
            //    .Build()
            //    .Run();
        }

        /// <summary>
        /// strong type
        /// </summary>
        private sealed class StringContentMiddleware : IMiddleware
        {
            private readonly string content;
            public StringContentMiddleware(string content) => this.content = content;

            public Task InvokeAsync(HttpContext context, RequestDelegate next) => context.Response.WriteAsync(content);
        }

        /// <summary>
        /// appointment middleware
        /// </summary>
        private sealed class StringContentMiddlewareAppointment
        {
            private readonly RequestDelegate next;
            private readonly string content;
            private readonly bool forward;

            public StringContentMiddlewareAppointment(RequestDelegate next, string content, bool forward = true)
            {
                this.next = next;
                this.content = content;
                this.forward = forward;
            }

            public async Task Invoke(HttpContext context)
            {
                await context.Response.WriteAsync(content);
                if (forward)
                {
                    await next(context);
                }
            }
        }
    }
}
