using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _ServiceHosting_06
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
                //.ConfigureHostConfiguration(builder => builder.AddJsonFile("hostEnvironment.json"))
                .ConfigureServices(services => services.AddHostedService<FakeHostedService>())
                .Build()
                .Run();

            /*
             dotnet run /environment=develop /contentroot=d:/temp /applicationname=App
             EnvironmentName:develop
             ApplicationName:App
             ContentRootPath:d:/temp
             */
        }
    }
}
