using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_04
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute { }
}
