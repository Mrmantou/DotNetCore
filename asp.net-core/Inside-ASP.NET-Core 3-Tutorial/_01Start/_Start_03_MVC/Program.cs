using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _Start_03_MVC
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(services => services
                        .AddRouting()
                        .AddControllersWithViews())
                    .Configure(app => app
                        .UseRouting() //Adds a Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware middleware
                        .UseEndpoints(endpoints => endpoints.MapControllers()))) //Adds a Microsoft.AspNetCore.Routing.EndpointMiddleware middleware 
                .Build()
                .Run();
        }
    }
}
