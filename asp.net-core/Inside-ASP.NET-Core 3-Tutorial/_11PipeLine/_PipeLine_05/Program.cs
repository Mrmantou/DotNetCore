using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _PipeLine_05
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(services => services
                        .AddSingleton<IFoo, Foo>()
                        .AddSingleton<IBar, Bar>()
                        .AddControllersWithViews())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
                .Build()
                .Run();
        }
    }

    public class HomeController : Controller
    {
        private readonly IFoo foo;
        public HomeController(IFoo foo) => this.foo = foo;

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.Foo = foo;
            return View();
        }
    }

    public interface IFoo { }
    public interface IBar { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
}
