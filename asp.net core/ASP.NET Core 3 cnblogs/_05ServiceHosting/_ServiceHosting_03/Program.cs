using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _ServiceHosting_03
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureServices(services => services.AddHostedService<FakeHostedService>())
                .Build()
                .Run();
        }
    }
}
