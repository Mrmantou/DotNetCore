using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _PipeLine_04
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>()
                    .ConfigureServices(services => services.AddSingleton<IFoo, Foo>())
                    .Configure(app => app.UseMiddleware<FooBarMiddleware>()))
                .Build()
                .Run();
        }
    }

    public class FooBarMiddleware : IMiddleware
    {
        public FooBarMiddleware(IFoo foo, IBar bar)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Debug.Assert(next != null);
            return Task.CompletedTask;
        }
    }

    public class FooBarConventionMiddleware
    {
        private RequestDelegate next;
        public FooBarConventionMiddleware(RequestDelegate next) => this.next = next;

        public Task InvokeAsync(HttpContext context, IFoo foo, IBar bar)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
            return Task.CompletedTask;
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            Debug.Assert(configuration != null);
            Debug.Assert(hostEnvironment != null);
            Debug.Assert(webHostEnvironment != null);
            Debug.Assert(ReferenceEquals(hostEnvironment, webHostEnvironment));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBar, Bar>();
        }

        public void Configure(IApplicationBuilder app, IFoo foo, IBar bar)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
        }
    }


    public interface IFoo { }
    public interface IBar { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
}
