using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_06
{
    public interface IFoobarbazgux { }

    public class Base : IDisposable
    {
        public Base() => Console.WriteLine($"Instance of {GetType().Name} is created.");
        public void Dispose() => Console.WriteLine($"Instance of {GetType().Name} is disposed.");
    }

    public class Foo : Base, IFoobarbazgux, IDisposable { }
    public class Bar : Base, IFoobarbazgux, IDisposable { }
    public class Baz : Base, IFoobarbazgux, IDisposable { }
    public class Gux : Base, IFoobarbazgux, IDisposable { }

}
