using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace _Configuration_01_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bind Object:");
            BindObject();

            Console.WriteLine("\nJson Config:");
            JsonConfig();
        }

        private static void JsonConfig()
        {
            var options = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("format")
                .Get<FormatOptions>();

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

        private static void BindObject()
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

            var options = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build()
                .GetSection("format")
                .Get<FormatOptions>();

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
    }
}
