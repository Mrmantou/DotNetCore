using System;
using System.Reflection;

namespace App
{
    public class ControllerActionDescriptor : ActionDescriptor
    {
        public Type ControllerType { get; set; }
        public MethodInfo Method { get; set; }
    }
}
