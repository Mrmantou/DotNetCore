using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HelloWorldApp
{
    public class Program
    {
        public static void Main() =>
            new WebHostBuilder()
                .UseKestrel()
                .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World")))
                .Build()
                .Run();
    }
}
