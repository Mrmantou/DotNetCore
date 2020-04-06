using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _Options_06
{
    class Program
    {
        static void Main(string[] args)
        {
            //OptionsExtentionDemo();


            TimeRefreshDemo();
        }

        private static void TimeRefreshDemo()
        {
            var random = new Random();
            var optionsMonitor = new ServiceCollection()
                .AddOptions()
                .Configure<FoobarOptions>(TimeSpan.FromSeconds(1))
                .Configure<FoobarOptions>(foobar =>
                {
                    foobar.Foo = random.Next(10, 100);
                    foobar.Bar = random.Next(10, 100);
                })
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<FoobarOptions>>();

            optionsMonitor.OnChange(foobar => Console.WriteLine($"[{DateTime.Now}] {foobar}"));

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }

        private static void OptionsExtentionDemo()
        {
            var foobar1 = new FoobarOptions(1, 1);
            var foobar2 = new FoobarOptions(2, 2);
            var foobar3 = new FoobarOptions(3, 3);

            var options = new ServiceCollection()
                .AddOptions()
                .Configure<FakeOptions>("fakeoptions.json")
                .BuildServiceProvider()
                .GetRequiredService<IOptions<FakeOptions>>()
                .Value;

            Debug.Assert(options.Foobar.Equals(foobar1));

            Debug.Assert(options.Array[0].Equals(foobar1));
            Debug.Assert(options.Array[1].Equals(foobar2));
            Debug.Assert(options.Array[2].Equals(foobar3));

            Debug.Assert(options.List[0].Equals(foobar1));
            Debug.Assert(options.List[1].Equals(foobar2));
            Debug.Assert(options.List[2].Equals(foobar3));

            Debug.Assert(options.Dictionary["1"].Equals(foobar1));
            Debug.Assert(options.Dictionary["2"].Equals(foobar2));
            Debug.Assert(options.Dictionary["3"].Equals(foobar3));
        }
    }
}
