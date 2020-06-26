﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace _PipeLine_03
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
        public void ConfigureServices(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            foreach (var service in services)
            {
                var serviceTypeName = GetName(service.ServiceType);
                var implementationType = service.ImplementationType
                    ?? service.ImplementationInstance?.GetType()
                    ?? service.ImplementationFactory?.Invoke(provider)?.GetType();

                if (implementationType != null)
                {
                    Console.WriteLine($"{service.Lifetime,-15}{serviceTypeName,-50}{GetName(implementationType)}");
                }
            }
        }

        public void Configure(IApplicationBuilder app) { }

        private string GetName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var name = type.Name.Split('`')[0];
            var args = type.GetGenericArguments().Select(i => i.Name);

            return $"{name}<{string.Join(",", args)}>";
        }
    }
}
