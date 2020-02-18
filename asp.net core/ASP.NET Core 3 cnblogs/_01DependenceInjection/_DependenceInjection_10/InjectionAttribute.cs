using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_10
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute { }
}
