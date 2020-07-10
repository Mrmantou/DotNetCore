using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace _PipeLine_08
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                ["FOOBAR:FOO"] = "Foo",
                ["FOOBAR:BAR"] = "Bar",
                ["Baz"] = "Baz"
            }).Build();

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    // --modify config
                    //.UseSetting("FOOBAR:FOO", "Foo")
                    //.UseSetting("FOOBAR:BAR", "Bar")
                    //.UseSetting("Baz", "Baz")
                    // --combine config
                    //.UseConfiguration(configuration)
                    // --register config source
                    .ConfigureAppConfiguration(config => config
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            ["FOOBAR:FOO"] = "Foo",
                            ["FOOBAR:BAR"] = "Bar",
                            ["Baz"] = "Baz"
                        }))
                    .UseSetting("urls", "http://0.0.0.0:8888;http://0.0.0.0:9999")
                    .UseStartup<Startup>())
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
