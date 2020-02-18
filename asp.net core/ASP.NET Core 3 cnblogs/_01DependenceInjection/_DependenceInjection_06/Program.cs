using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Diagnostics;
using System.Linq;

namespace _DependenceInjection_06
{
    class Program
    {
        static void Main(string[] args)
        {
            TryAddEnumerableTest();

            RemoveAndReplaceTest();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }

        private static void RemoveAndReplaceTest()
        {
            Console.WriteLine("\nRemoveAndReplaceTest:");
            var services = new ServiceCollection();
            services.Replace(ServiceDescriptor.Singleton<IFoobarbazgux, Foo>());
            Debug.Assert(services.Any(item => item.ImplementationType == typeof(Foo)));

            services.AddSingleton<IFoobarbazgux, Bar>();
            services.Replace(ServiceDescriptor.Singleton<IFoobarbazgux, Baz>());

            Debug.Assert(!services.Any(item => item.ImplementationType == typeof(Foo)));
            Debug.Assert(services.Any(item => item.ImplementationType == typeof(Bar)));
            Debug.Assert(services.Any(item => item.ImplementationType == typeof(Baz)));
        }

        private static void TryAddEnumerableTest()
        {
            Console.WriteLine("\nTryAddEnumerableTest:");
            var services = new ServiceCollection();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux, Foo>());
            Debug.Assert(services.Count == 1);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux, Foo>());
            Debug.Assert(services.Count == 1);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux>(new Foo()));
            Debug.Assert(services.Count == 1);

            Func<IServiceProvider, Foo> factory4Foo = _ => new Foo();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux>(factory4Foo));
            Debug.Assert(services.Count == 1);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux, Bar>());
            Debug.Assert(services.Count == 2);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux>(new Baz()));
            Debug.Assert(services.Count == 3);

            Func<IServiceProvider, Gux> factory4Gux = _ => new Gux();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFoobarbazgux>(factory4Gux));
            Debug.Assert(services.Count == 4);
        }
    }
}
