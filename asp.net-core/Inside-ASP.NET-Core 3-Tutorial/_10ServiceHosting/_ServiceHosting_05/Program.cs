using _ServiceHosting_05.MiniCat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace _ServiceHosting_05
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureServices(service => service.AddHostedService<FakeHostedService>())
                .UseServiceProviderFactory(new CatServiceProviderFactory())
                .ConfigureContainer<CatBuilder>(builder => builder.Register(Assembly.GetEntryAssembly()))
                .Build()
                .Run();
        }
    }
}
