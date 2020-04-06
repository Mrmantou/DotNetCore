using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace _Options_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var foobar1 = new FoobarOptions(1, 1);
            var foobar2 = new FoobarOptions(1, 2);
            var foobar3 = new FoobarOptions(1, 3);

            var options = new ServiceCollection()
                .AddOptions()
                .Configure<FakeOptions>("fackoptions.json")
                .BuildServiceProvider()
                .GetRequiredService<IOptions<FakeOptions>>()
                .Value;
        }
    }  
}
