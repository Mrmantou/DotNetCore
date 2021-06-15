using System;
using Volo.Abp.DependencyInjection;

namespace _04_abp_MoodyHandler
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
