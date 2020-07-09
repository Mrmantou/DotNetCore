using _PipeLine_06.MiniCat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace _PipeLine_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>())
                .UseServiceProviderFactory(new CatServiceProviderFactory())
                .Build()
                .Run();
        }
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app, IFoo foo, IBar bar, IBaz baz)
        {
            app.Run(async context =>
            {
                var response = context.Response;
                response.ContentType = "text/html";
                await response.WriteAsync($"foo: {foo}<br/>");
                await response.WriteAsync($"bar: {bar}<br/>");
                await response.WriteAsync($"baz: {baz}<br/>");
            });
        }

        public void ConfigureContainer(CatBuilder container) => container.Register(Assembly.GetEntryAssembly());
    }

    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }

    [MapTo(typeof(IFoo), LifeTime.Root)]
    public class Foo : IFoo { }
    [MapTo(typeof(IBar), LifeTime.Root)]
    public class Bar : IBar { }
    [MapTo(typeof(IBaz), LifeTime.Root)]
    public class Baz : IBaz { }
}
