using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_07
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux { }


    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }

    public class Gux : IGux
    {
        public Gux(IFoo foo) => Console.WriteLine("Selected constructor: Gux(IFoo)");
        public Gux(IFoo foo, IBar bar) => Console.WriteLine("Selected constructor: Gux(IFoo, IBar)");
        public Gux(IFoo foo, IBar bar, IBaz baz) => Console.WriteLine("Selected constructor: Gux(IFoo, IBar, IBaz)");
    }

    public class GuxEx : IGux
    {
        public GuxEx(IFoo foo, IBar bar) => Console.WriteLine("Selected constructor: Gux(IFoo, IBar)");
        public GuxEx(IFoo foo, IBaz baz) => Console.WriteLine("Selected constructor: Gux(IFoo,  IBaz)");
    }

}
