using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _Configuration_04
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new Dictionary<string, string>
            {
                ["foo"] = null,
                ["bar"] = "",
                ["baz"] = "123"
            };

            var root = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            // for object
            Debug.Assert(root.GetValue<object>("foo") == null);
            Debug.Assert("".Equals(root.GetValue<object>("bar")));
            Debug.Assert("123".Equals(root.GetValue<object>("baz")));

            // for general type
            Debug.Assert(root.GetValue<int>("foo") == 0);
            Debug.Assert(root.GetValue<int>("baz") == 123);

            // for Nullable<T>
            Debug.Assert(root.GetValue<int?>("foo") == null);
            Debug.Assert(root.GetValue<int?>("bar") == null);

        }
    }
}
