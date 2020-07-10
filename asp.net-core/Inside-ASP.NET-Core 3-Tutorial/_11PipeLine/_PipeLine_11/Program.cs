using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace _PipeLine_11
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    //.ConfigureServices((context, services) =>
                    //{
                    //    if (context.HostingEnvironment.IsDevelopment())
                    //    {
                    //        services.AddSingleton<IFoobar, Foo>();
                    //    }
                    //    else
                    //    {
                    //        services.AddSingleton<IFoobar, Bar>();
                    //    }
                    //})
                    .UseStartup<Startup>()
                    .Configure(app =>
                    {
                        //var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                        //if (environment.IsDevelopment())
                        //{
                        //    app.UseMiddleware<FooMiddleware>();
                        //}
                        //else
                        //{
                        //    app.UseMiddleware<BarMiddleware>();
                        //}

                        //app.UseWhen(
                        //    context => context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment(),
                        //    builder => builder.UseMiddleware<FooMiddleware>())
                        //.UseMiddleware<BarMiddleware>()
                        //.UseMiddleware<BazMiddleware>();
                    })
                    .ConfigureAppConfiguration((context, config) => config
                        .AddJsonFile(path: "AppSettings.json", optional: false)
                        .AddJsonFile(path: $"AppSettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)))
                .Build()
                .Run();
        }
    }

    public class Startup
    {
        private readonly IWebHostEnvironment environment;

        public Startup(IWebHostEnvironment environment) => this.environment = environment;

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    if (environment.IsDevelopment())
        //    {
        //        services.AddSingleton<IFoobar, Foo>();
        //    }
        //    else
        //    {
        //        services.AddSingleton<IFoobar, Bar>();
        //    }
        //}

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton<IFoobar, Foo>();
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            services.AddSingleton<IFoobar, Bar>();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddSingleton<IFoobar, Baz>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            if (environment.IsDevelopment()) //if (webHostEnvironment.IsDevelopment())
            {
                app.UseMiddleware<FooMiddleware>();
            }
        }

        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseMiddleware<FooMiddleware>();
        }
    }

    public class FooMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            return Task.CompletedTask;
        }
    }

    public class BarMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            return Task.CompletedTask;
        }
    }

    public class BazMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            return Task.CompletedTask;
        }
    }

    public interface IFoobar { }

    public class Foo : IFoobar { }
    public class Bar : IFoobar { }
    public class Baz : IFoobar { }
}
