using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;

namespace _Configuration_01_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Memory Config Demo:");
            MemoryConfigDemo();

            Console.WriteLine("\nConfiguration Tree Demo:");
            ConfigurationTreeDemo();
        }

        private static void ConfigurationTreeDemo()
        {
            var source = new Dictionary<string, string>
            {
                ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
                ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
                ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
                ["format:dateTime:shortTimePattern"] = "h:mm tt",

                ["format:currencyDecimal:digits"] = "2",
                ["format:currencyDecimal:symbol"] = "$"
            };

            var config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            var options = new FormatOptions(config.GetSection("Format"));
            var dateTime = options.DateTime;
            var currencyDecimal = options.CurrencyDecimal;
            Console.WriteLine("DateTime:");
            Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
            Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
            Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
            Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

            Console.WriteLine("CurrencyDecimal:");
            Console.WriteLine($"\tDigits: {currencyDecimal.Digits}");
            Console.WriteLine($"\tSymbol: {currencyDecimal.Symbol}");
        }

        private static void MemoryConfigDemo()
        {
            var source = new Dictionary<string, string>
            {
                ["longDatePattern"] = "dddd, MMMM d, yyyy",
                ["longTimePattern"] = "h:mm:ss tt",
                ["shortDatePattern"] = "M/d/yyyy",
                ["shortTimePattern"] = "h:mm tt"
            };

            var config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            var options = new DateTimeFormatOptions(config);
            Console.WriteLine($"LongDatePattern: {options.LongDatePattern}");
            Console.WriteLine($"LongTimePattern: {options.LongTimePattern}");
            Console.WriteLine($"ShortDatePattern: {options.ShortDatePattern}");
            Console.WriteLine($"ShortTimePattern: {options.ShortTimePattern}");
        }
    }
}
