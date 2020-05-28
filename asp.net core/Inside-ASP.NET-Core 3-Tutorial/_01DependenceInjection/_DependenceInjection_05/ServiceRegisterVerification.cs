using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_05
{
    public class ServiceRegisterVerification
    {
        public void TestValidateScopes()
        {
            var root = new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddScoped<IBar, Bar>()
                .BuildServiceProvider(true);

            var child = root.CreateScope().ServiceProvider;

            void ResolveService<T>(IServiceProvider provider)
            {
                var isRootContainer = root == provider ? "Yes" : "No";

                try
                {
                    provider.GetService<T>();
                    Console.WriteLine($"Status: Success; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
                    Console.WriteLine($"Error:{ex.Message}");
                }
            }

            ResolveService<IFoo>(root);
            ResolveService<IBar>(root);
            ResolveService<IFoo>(child);
            ResolveService<IBar>(child);
        }

        public void TestValidateOnBuild()
        {
            BuildServiceProvider(true);
            BuildServiceProvider(false);

            static void BuildServiceProvider(bool validateOnBuild)
            {
                try
                {
                    var options = new ServiceProviderOptions
                    {
                        ValidateOnBuild = validateOnBuild
                    };

                    new ServiceCollection()
                        .AddSingleton<IFoobar, Foobar>()
                        .BuildServiceProvider(options);

                    Console.WriteLine($"Status: Success; ValidateOnBuild: {validateOnBuild}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; ValidateOnBuild: {validateOnBuild}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public interface IFoo { }
        public interface IBar { }
        public interface IFoobar { }

        public class Foobar : IFoobar
        {
            private Foobar() { }
            public static readonly Foobar Instance = new Foobar();
        }

        public class Foo : IFoo
        {
            public IBar Bar { get; }

            public Foo(IBar bar) => Bar = bar;
        }

        public class Bar : IBar { }
    }
}
