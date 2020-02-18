using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_10
{
    public class ServiceRegistry
    {
        public Type ServiceType { get; }
        public LifeTime LifeTime { get; }
        public Func<Cat, Type[], object> Factory { get; }
        internal ServiceRegistry Next { get; set; }

        public ServiceRegistry(Type serviceType, LifeTime lifeTime, Func<Cat, Type[], object> factory)
        {
            ServiceType = serviceType;
            LifeTime = lifeTime;
            Factory = factory;
        }

        internal IEnumerable<ServiceRegistry> AsEnumerable()
        {
            var list = new List<ServiceRegistry>();

            for (var self = this; self != null; self = self.Next)
            {
                list.Add(self);
            }

            return list;
        }
    }
}
