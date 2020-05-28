using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace _ServiceHosting_05.MiniCat
{
    public class CatBuilder
    {
        private readonly Cat cat;

        public CatBuilder(Cat cat)
        {
            this.cat = cat;
            cat.Register<IServiceScopeFactory>(c => new ServiceScopeFactory(c.CreateChild()), LifeTime.Transient);
        }

        public IServiceProvider BuilderServiceProvider() => cat;

        public CatBuilder Register(Assembly assembly)
        {
            cat.Register(assembly);
            return this;
        }

        private class ServiceScope : IServiceScope
        {
            public ServiceScope(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

            public IServiceProvider ServiceProvider { get; }

            public void Dispose() => (ServiceProvider as IDisposable)?.Dispose();
        }

        private class ServiceScopeFactory : IServiceScopeFactory
        {
            private readonly Cat cat;

            public ServiceScopeFactory(Cat cat) => this.cat = cat;

            public IServiceScope CreateScope() => new ServiceScope(cat);
        }
    }
}
