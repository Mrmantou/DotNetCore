using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace _PipeLine_09
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>())
                .Build()
                .Run();
        }
    }

    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            //need publish
            Debug.Assert(environment.ContentRootPath == AppDomain.CurrentDomain.BaseDirectory);
            Debug.Assert(environment.ContentRootPath == AppContext.BaseDirectory);

            var wwwroot = Path.Combine(environment.ContentRootPath, "wwwroot");
            if (Directory.Exists(wwwroot))
            {
                Debug.Assert(environment.WebRootPath == wwwroot);
            }
            else
            {
                Debug.Assert(environment.WebRootPath == null);
            }
        }

        public void Configure(IApplicationBuilder app) { }
    }
}
