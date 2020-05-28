using System;
using System.Reflection;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Type widgetType = typeof(Widget);

            object[] widgetClassAttributes = widgetType.GetCustomAttributes(typeof(HelpAttribute), false);
            if (widgetClassAttributes.Length > 0)
            {
                HelpAttribute attr = (HelpAttribute)widgetClassAttributes[0];
                Console.WriteLine("Widget class help URL : " + attr.Url + " - Related topic : " + attr.Topic);
            }

            MethodInfo displayMehod = widgetType.GetMethod(nameof(Widget.Display));
            object[] displayMethodAttributes = displayMehod.GetCustomAttributes(typeof(HelpAttribute), false);
            if (displayMethodAttributes.Length > 0)
            {
                HelpAttribute attr = (HelpAttribute)displayMethodAttributes[0];
                Console.WriteLine("Display method help URL : " + attr.Url + " - Related topic : " + attr.Topic);
            }


            Console.WriteLine("Hello World!");

            // Console.WriteLine("press any key to exit...");
            // Console.ReadKey();
        }

        private static void TestOne()
        {
            // TypeInfo typeInfo = typeof(MyClass).GetTypeInfo();
            // Console.WriteLine("The assembly qualified name of MyClass is " + typeInfo.AssemblyQualifiedName);

            // var attrs = typeInfo.GetCustomAttributes();

            // foreach (var item in attrs)
            // {
            //     System.Console.WriteLine("Attribute on MyClass: " + item.GetType().Name);
            // }
        }
    }
}
