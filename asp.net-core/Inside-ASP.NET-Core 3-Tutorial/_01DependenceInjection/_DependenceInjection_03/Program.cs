using System;
using System.Threading.Tasks;

namespace _DependenceInjection_03
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cat = new Cat();
            cat.Register<IControllerActivator, SingletonControllerActivator>();

            var engine = new MvcEngine(cat);
            var address = new Uri("http://0.0.0.0:8080/mvcapp");
            await engine.StartAsync(address);

            Console.WriteLine("Press any key to exit......");
            Console.ReadKey();
        }
    }
}
