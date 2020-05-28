using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace _DependenceInjection_04
{
    class Program
    {
        static void Main(string[] args)
        {
            NormalService();

            GenericService();

            MultipleService();

            InstanceLifeCycle();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }

        private static void InstanceLifeCycle()
        {
            using (var root = new Cat().Register<IFoo, Foo>(LifeTime.Transient)
                            .Register<IBar>(_ => new Bar(), LifeTime.Self)
                            .Register<IBaz, Baz>(LifeTime.Root)
                            .Register(Assembly.GetExecutingAssembly()))
            {
                using (var cat = root.CreateChild())
                {
                    cat.GetService<IFoo>();
                    cat.GetService<IBar>();
                    cat.GetService<IBaz>();
                    cat.GetService<IGux>();

                    Console.WriteLine("Child cat is disposed.");
                }

                Console.WriteLine("Root cat is disposed.");
            }
        }

        private static void MultipleService()
        {
            var services = new Cat()
                .Register<Base, Foo>(LifeTime.Transient)
                .Register<Base, Bar>(LifeTime.Transient)
                .Register<Base, Baz>(LifeTime.Transient)
                .GetServices<Base>();

            Debug.Assert(services.OfType<Foo>().Any());
            Debug.Assert(services.OfType<Bar>().Any());
            Debug.Assert(services.OfType<Baz>().Any());
        }

        private static void GenericService()
        {
            var cat = new Cat()
                .Register<IFoo, Foo>(LifeTime.Transient)
                .Register<IBar, Bar>(LifeTime.Transient)
                .Register(typeof(IFoobar<,>), typeof(Foobar<,>), LifeTime.Transient);

            var foobar = (Foobar<IFoo, IBar>)cat.GetService<IFoobar<IFoo, IBar>>();
            Debug.Assert(foobar.Foo is Foo);
            Debug.Assert(foobar.Bar is Bar);
        }

        private static void NormalService()
        {
            var root = new Cat().Register<IFoo, Foo>(LifeTime.Transient)
                            .Register<IBar>(_ => new Bar(), LifeTime.Self)
                            .Register<IBaz, Baz>(LifeTime.Root)
                            .Register(Assembly.GetExecutingAssembly());

            var cat1 = root.CreateChild();
            var cat2 = root.CreateChild();

            void GetServices<TService>(Cat cat)
            {
                cat.GetService<TService>();
                cat.GetService<TService>();
            }

            GetServices<IFoo>(cat1);
            GetServices<IBar>(cat1);
            GetServices<IBaz>(cat1);
            GetServices<IGux>(cat1);
            Console.WriteLine();
            GetServices<IFoo>(cat2);
            GetServices<IBar>(cat2);
            GetServices<IBaz>(cat2);
            GetServices<IGux>(cat2);
        }
    }
}
