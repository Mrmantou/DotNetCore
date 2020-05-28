using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace _DependenceInjection_04
{
    public static class CatExtensions
    {
        public static Cat Register(this Cat cat, Type from, Type to, LifeTime lifeTime)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => Create(_, to, arguments);
            cat.Register(new ServiceRegistry(from, lifeTime, factory));
            return cat;
        }

        public static Cat Register<TFrom, TTo>(this Cat cat, LifeTime lifeTime) where TTo : TFrom
            => cat.Register(typeof(TFrom), typeof(TTo), lifeTime);

        public static Cat Register(this Cat cat, Type serviceType, object instance)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => instance;
            cat.Register(new ServiceRegistry(serviceType, LifeTime.Root, factory));
            return cat;
        }

        public static Cat Register<TService>(this Cat cat, object instance)
            => cat.Register(typeof(TService), instance);

        public static Cat Register(this Cat cat, Type serviceType, Func<Cat, object> factory, LifeTime lifeTime)
        {
            cat.Register(new ServiceRegistry(serviceType, lifeTime, (_, arguments) => factory(_)));
            return cat;
        }

        public static Cat Register<TService>(this Cat cat, Func<Cat, TService> factory, LifeTime lifeTime)
        {
            cat.Register(new ServiceRegistry(typeof(TService), lifeTime, (_, arguments) => factory(_)));
            return cat;
        }

        public static Cat Register(this Cat cat, Assembly assembly)
        {
            var typedAttributes = from type in assembly.GetExportedTypes()
                                  let attribute = type.GetCustomAttribute<MapToAttribute>()
                                  where attribute != null
                                  select new { ServiceType = type, Attribute = attribute };

            foreach (var typedAttribute in typedAttributes)
            {
                cat.Register(typedAttribute.Attribute.ServiceType, typedAttribute.ServiceType, typedAttribute.Attribute.LifeTime);
            }
            return cat;
        }

        public static Cat CreateChild(this Cat cat) => new Cat(cat);

        public static TService GetService<TService>(this Cat cat) => (TService)cat.GetService(typeof(TService));

        public static IEnumerable<TService> GetServices<TService>(this Cat cat) => cat.GetService<IEnumerable<TService>>();

        private static object Create(Cat cat, Type type, Type[] genericArguments)
        {
            if (genericArguments.Length > 0)
            {
                type = type.MakeGenericType(genericArguments);
            }
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"Cannot create the instance of {type} which does not have a public constructor.");
            }
            var constructor = constructors.FirstOrDefault(item => item.GetCustomAttributes(false).OfType<InjectionAttribute>().Any());
            constructor ??= constructors.First();
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            var arguments = new object[parameters.Length];
            for (int index = 0; index < arguments.Length; index++)
            {
                arguments[index] = cat.GetService(parameters[index].ParameterType);
            }

            return constructor.Invoke(arguments);
        }
    }
}
