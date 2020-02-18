using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_10
{
    public class CatServiceProviderFactory : IServiceProviderFactory<CatBuilder>
    {
        public CatBuilder CreateBuilder(IServiceCollection services)
        {
            var cat = new Cat();

            foreach (var service in services)
            {
                if (service.ImplementationFactory != null)
                {
                    cat.Register(service.ServiceType, provider => service.ImplementationFactory(provider), service.Lifetime.AsCatLifetime());
                }
                else if (service.ImplementationInstance != null)
                {
                    cat.Register(service.ServiceType, service.ImplementationInstance);
                }
                else
                {
                    cat.Register(service.ServiceType, service.ImplementationType, service.Lifetime.AsCatLifetime());
                }
            }

            return new CatBuilder(cat);
        }

        public IServiceProvider CreateServiceProvider(CatBuilder containerBuilder) => containerBuilder.BuilderServiceProvider();
    }

    internal static class Extensions
    {
        public static LifeTime AsCatLifetime(this ServiceLifetime lifetime)
        {
            return lifetime switch
            {
                ServiceLifetime.Scoped => LifeTime.Self,
                ServiceLifetime.Singleton => LifeTime.Root,
                _ => LifeTime.Transient
            };
        }
    }
}
