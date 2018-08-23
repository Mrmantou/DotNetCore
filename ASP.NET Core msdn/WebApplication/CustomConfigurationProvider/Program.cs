using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CustomConfigurationProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEntityFrameworkConfig(options =>//EntityFrameworkd配置会覆盖JSonFile的配置
                options.UseSqlServer(connectionStringConfig.GetConnectionString("DefaultConnection")))
                .Build();

            Console.WriteLine("key1={0}", config["key1"]);
            Console.WriteLine("key2={0}", config["key2"]);
            Console.WriteLine("key3={0}", config["key3"]);
            Console.WriteLine();

            Console.WriteLine("Press a key...");
            Console.ReadKey();
        }
    }
}
