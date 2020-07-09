using System;
using System.Collections.Generic;
using System.Text;

namespace _PipeLine_06.MiniCat
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute { }
}
