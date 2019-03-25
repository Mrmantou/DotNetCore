using System;
using System.Reflection;

namespace HelloWorld
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MyAttributeForClassAndStructOnly : Attribute
    {

    }
}