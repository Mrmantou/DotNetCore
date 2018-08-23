using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, string>
            {
               {"Profile:MachineName", "MairaPC"},
                {"App:MainWindow:Left", "1980"}
            };

            var builder = new ConfigurationBuilder();

            builder.AddInMemoryCollection(dict).AddCommandLine(args);

            var config = builder.Build();

            Console.WriteLine($"MachineName {config["Profile:MachineName"]}");

            Console.WriteLine($"Left {config["App:MainWindow:Left"]}");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
