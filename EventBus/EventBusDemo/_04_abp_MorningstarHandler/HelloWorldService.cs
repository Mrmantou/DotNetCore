using System;
using Volo.Abp.DependencyInjection;

namespace _04_abp_MorningstarHandler
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
