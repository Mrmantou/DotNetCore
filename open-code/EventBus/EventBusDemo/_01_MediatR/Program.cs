using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _01_MediatR
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            
            services.AddMvc();
            services.AddMediatR(typeof(Program));
            
            var serviceProvider = services.BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            Console.WriteLine($"Main thread id: {Thread.CurrentThread.ManagedThreadId}");

            //var response = await mediator.Send(new Ping());

            //await mediator.Send(new OneWay());

            await mediator.Publish(new Pinged());

            Console.ReadKey();
        }
    }
}
