using System;
using System.Threading.Tasks;

namespace _DependenceInjection_02_3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var address = new Uri("http://0.0.0.0:8080/mvcapp");
            var engine = new MvcEngine(new FoobarEngineFactory());

            await engine.StartAsync(address);

            Console.WriteLine("Press any key to exit......");
            Console.ReadKey();
        }
    }
}
