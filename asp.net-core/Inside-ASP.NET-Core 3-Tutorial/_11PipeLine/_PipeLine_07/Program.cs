using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace _PipeLine_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_FOOBAR:FOO", "Foo");
            Environment.SetEnvironmentVariable("ASPNETCORE_FOOBAR:BAR", "Bar");
            Environment.SetEnvironmentVariable("ASPNETCORE_Baz", "Baz");

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                .Build()
                .Run();
        }
    }

    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration) => this.configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services.Configure<FoobarOptions>(configuration);

        public void Configure(IApplicationBuilder app, IOptions<FoobarOptions> optionsAccessor)
        {
            var options = optionsAccessor.Value;

            var json = JsonConvert.SerializeObject(options, Formatting.Indented);

            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync($"<pre>{json}</pre>");
            });
        }
    }

    public class FoobarOptions
    {
        public Foobar Foobar { get; set; }
        public string Baz { get; set; }
    }

    public class Foobar
    {
        public string Foo { get; set; }
        public string Bar { get; set; }
    }
}
