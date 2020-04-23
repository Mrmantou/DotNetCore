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
        private static readonly string html =
@"<html>
<head>
    <title>Hello</title>
</head>
<body>
    <p>Hello World!</p>
</body>
</html>";

        [HttpGet("/{foo}")]
        public Task<IActionResult> FooAsync()
        {
            return Task.FromResult<IActionResult>(new ContentResult(html, "text/html"));
        }

        public IActionResult Bar() => new ContentResult(html, "text/plain");
    }
}
