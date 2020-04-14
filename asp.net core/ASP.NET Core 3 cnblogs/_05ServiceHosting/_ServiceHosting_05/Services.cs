using _ServiceHosting_05.MiniCat;
using System;
using System.Collections.Generic;
using System.Text;

namespace _ServiceHosting_05
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public class Base : IDisposable
    {
        public Base() => Console.WriteLine($"Instance of {GetType().Name} is created.");
        public void Dispose() => Console.WriteLine($"Instance of {GetType().Name} is disposed.");
    }

    [MapTo(typeof(IFoo), LifeTime.Root)]
    public class Foo : Base, IFoo { }

    [MapTo(typeof(IBar), LifeTime.Root)]
    public class Bar : Base, IBar { }

    [MapTo(typeof(IBaz), LifeTime.Root)]
    public class Baz : Base, IBaz { }

}
