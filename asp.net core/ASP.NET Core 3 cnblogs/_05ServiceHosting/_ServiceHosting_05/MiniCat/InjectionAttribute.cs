using System;
using System.Collections.Generic;
using System.Text;

namespace _ServiceHosting_05.MiniCat
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute { }
}
