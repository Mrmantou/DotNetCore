using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        private static readonly string html =
@"<html>
<head>
    <title>Hello</title>
</head>
<body>
    <p>Hello World!</p>
</body>
</html>";

        [HttpGet("/foo")]
        public Task<ContentResult> FooAsync() => Task.FromResult(new ContentResult(html, "text/html"));

        [HttpGet("/bar")]
        public ValueTask<ContentResult> BarAsync() => new ValueTask<ContentResult>(new ContentResult(html, "text/html"));

        [HttpGet("/baz")]
        public Task<string> BazAsync() => Task.FromResult(html);

        [HttpGet("/qux")]
        public ValueTask<string> QuxAsync() => new ValueTask<string>(html);
    }


    public class HomeController
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        public string Action1(string foo, int bar, double baz) => JsonSerializer.Serialize(new { Foo = foo, Bar = bar, Baz = baz }, options);
        public string Action2(Foobarbaz value1, Foobarbaz value2) => JsonSerializer.Serialize(new { Value1 = value1, Value2 = value2 }, options);
        public string Action3(Foobarbaz value1, [FromBody]Foobarbaz value2) => JsonSerializer.Serialize(new { Value1 = value1, Value2 = value2 }, options);
    }

    public class Foobarbaz
    {
        public Foobar Foobar { get; set; }
        public double Baz { get; set; }
    }

    public class Foobar
    {
        public string Foo { get; set; }
        public int Bar { get; set; }
    }
}
