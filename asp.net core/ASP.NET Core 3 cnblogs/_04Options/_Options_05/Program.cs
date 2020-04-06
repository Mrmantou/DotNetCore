using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace _Options_05
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<FoobarOptions>(foobar =>
                {
                    foobar.Foo = random.Next(1, 100);
                    foobar.Bar = random.Next(1, 100);
                })
                .BuildServiceProvider();

            Print(serviceProvider);
            Print(serviceProvider);

            /*
             options:Foo:62, Bar:26
             optionsSnapshot1:Foo:23, Bar:34
             optionsSnapshot2:Foo:23, Bar:34
             
             options:Foo:62, Bar:26
             optionsSnapshot1:Foo:10, Bar:42
             optionsSnapshot2:Foo:10, Bar:42
             */

            static void Print(IServiceProvider provider)
            {
                var scopedProvider = provider
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope()
                    .ServiceProvider;

                var options = scopedProvider
                    .GetRequiredService<IOptions<FoobarOptions>>()
                    .Value;

                var optionsSnapshot1 = scopedProvider
                    .GetRequiredService<IOptionsSnapshot<FoobarOptions>>()
                    .Value;
                var optionsSnapshot2 = scopedProvider
                    .GetRequiredService<IOptionsSnapshot<FoobarOptions>>()
                    .Value;

                Console.WriteLine($"options:{options}");
                Console.WriteLine($"optionsSnapshot1:{optionsSnapshot1}");
                Console.WriteLine($"optionsSnapshot2:{optionsSnapshot2}\n");
            }

        }
    }


    class FoobarOptions
    {
        public int Foo { get; set; }
        public int Bar { get; set; }
        public override string ToString() => $"Foo:{Foo}, Bar:{Bar}";
    }
}
