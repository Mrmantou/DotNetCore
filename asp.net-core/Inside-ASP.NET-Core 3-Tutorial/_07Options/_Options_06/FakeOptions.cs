using System;
using System.Collections.Generic;
using System.Text;

namespace _Options_06
{
    public class FakeOptions
    {
        public FoobarOptions Foobar { get; set; }
        public FoobarOptions[] Array { get; set; }
        public IList<FoobarOptions> List { get; set; }
        public IDictionary<string, FoobarOptions> Dictionary { get; set; }
    }

    public class FoobarOptions : IEquatable<FoobarOptions>
    {
        public int Foo { get; set; }
        public int Bar { get; set; }

        public FoobarOptions() { }
        public FoobarOptions(int foo, int bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public override string ToString() => $"Foo:{Foo}, Bar:{Bar}";
        public bool Equals(FoobarOptions other) => this.Foo == other?.Foo && this.Bar == other?.Bar;
    }
}
