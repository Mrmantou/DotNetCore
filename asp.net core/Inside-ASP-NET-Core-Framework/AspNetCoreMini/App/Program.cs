using System;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static async Task Main() => await new WebHostBuilder()
            .UseHttpListener()
            .Configure(app => app
                .Use(FooMiddleware)
                .Use(BarMiddlerware)
                .Use(BazMiddleware))
            .Build()
            .StartAsync();

        static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        static RequestDelegate BarMiddlerware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Bar=>");
            await next(context);
        };

        static RequestDelegate BazMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Baz");
        };
    }
}
