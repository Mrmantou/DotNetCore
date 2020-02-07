using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;

namespace _DependenceInjection_05
{
    class Program
    {
        static void Main(string[] args)
        {
            //RegistAndConsume();
            //Console.WriteLine();

            //GenericService();
            //Console.WriteLine();

            //MultipleServices();
            //Console.WriteLine();

            //InstanceLifeCycle();
            //Console.WriteLine();

            //InstanceLifeCycleDispose();
            //Console.WriteLine();

            new ServiceRegisterVerification().TestValidateScopes();
            Console.WriteLine();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }

        private static void InstanceLifeCycleDispose()
        {
            using (var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider())
            {
                using (var scope = root.CreateScope())
                {
                    var provider = scope.ServiceProvider;
                    provider.GetService<IFoo>();
                    provider.GetService<IBar>();
                    provider.GetService<IBaz>();
                    Console.WriteLine("Child container is disposed.");
                }
                Console.WriteLine("Root container is disposed.");
            }
        }

        private static void InstanceLifeCycle()
        {
            var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();

            var provider1 = root.CreateScope().ServiceProvider;
            var provider2 = root.CreateScope().ServiceProvider;

            GetServices<IFoo>(provider1);
            GetServices<IBar>(provider1);
            GetServices<IBaz>(provider1);
            Console.WriteLine();
            GetServices<IFoo>(provider2);
            GetServices<IBar>(provider2);
            GetServices<IBaz>(provider2);

            static void GetServices<T>(IServiceProvider provider)
            {
                provider.GetService<T>();
                provider.GetService<T>();
            }
        }

        private static void MultipleServices()
        {
            var provider = new ServiceCollection()
                .AddTransient<Base, Foo>()
                .AddTransient<Base, Bar>()
                .AddTransient<Base, Baz>()
                .BuildServiceProvider();

            var service = provider.GetService<Base>();
            Debug.Assert(service is Baz);

            var services = provider.GetServices<Base>();

            Debug.Assert(services.OfType<Foo>().Any());
            Debug.Assert(services.OfType<Bar>().Any());
            Debug.Assert(services.OfType<Baz>().Any());
        }

        private static void GenericService()
        {
            var provider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient(typeof(IFoobar<,>), typeof(Foobar<,>))
                .BuildServiceProvider();

            var foobar = (Foobar<IFoo, IBar>)provider.GetService<IFoobar<IFoo, IBar>>();
            Debug.Assert(foobar.Foo is Foo);
            Debug.Assert(foobar.Bar is Bar);
        }

        private static void RegistAndConsume()
        {
            var provider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();

            Debug.Assert(provider.GetService<IFoo>() is Foo);
            Debug.Assert(provider.GetService<IBar>() is Bar);
            Debug.Assert(provider.GetService<IBaz>() is Baz);
        }
    }
}
