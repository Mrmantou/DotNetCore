using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _Configuration_03
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new Dictionary<string, string>
            {
                ["A:B:C"] = "ABC"
            };

            var root = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            var section1 = root.GetSection("A:B:C");
            var section2 = root.GetSection("A:B").GetSection("C");
            var section3 = root.GetSection("A").GetSection("B:C");

            Debug.Assert(section1.Value == "ABC");
            Debug.Assert(section2.Value == "ABC");
            Debug.Assert(section3.Value == "ABC");

            Debug.Assert(!ReferenceEquals(section1, section2));
            Debug.Assert(!ReferenceEquals(section1, section3));
            Debug.Assert(null != root.GetSection("D"));
        }
    }
}
