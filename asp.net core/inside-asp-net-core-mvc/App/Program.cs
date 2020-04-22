using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                    builder.ConfigureServices(services => services
                                .AddRouting()
                                .AddMvcControllers())
                           .Configure(app => app
                                .UseDeveloperExceptionPage()
                                .UseRouting()
                                .UseEndpoints(endpoints =>
                                    endpoints.MapMvcControllerRoute("default", "{controller}/{action}"))))
                .Build()
                .Run();
        }
    }

    public class FoobarController : Controller
    {
        [HttpGet("/foo")]
        public Task FooAsync() => ActionContext.HttpContext.Response.WriteAsync(nameof(FooAsync));
        public Task BarAsync() => ActionContext.HttpContext.Response.WriteAsync(nameof(BarAsync));
    }
}
