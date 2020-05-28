using Microsoft.Extensions.DependencyInjection;
using System;

namespace _DependenceInjection_07
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceCreateTest();
            ServiceCreateConflictTest();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }

        private static void ServiceCreateTest()
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IGux, Gux>()
                .BuildServiceProvider()
                .GetService<IGux>();


        }

        private static void ServiceCreateConflictTest()
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IBaz, Baz>()
                .AddTransient<IGux, GuxEx>()
                .BuildServiceProvider()
                .GetService<IGux>();
        }
    }
}
