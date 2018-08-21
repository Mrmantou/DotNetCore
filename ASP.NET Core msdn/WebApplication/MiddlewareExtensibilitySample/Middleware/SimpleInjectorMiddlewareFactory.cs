using Microsoft.AspNetCore.Http;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample.Middleware
{
    public class SimpleInjectorMiddlewareFactory : IMiddlewareFactory
    {
        private readonly Container container;

        public SimpleInjectorMiddlewareFactory(Container container)
        {
            this.container = container;
        }

        public IMiddleware Create(Type middlewareType)
        {
            return container.GetInstance(middlewareType) as IMiddleware;
        }

        public void Release(IMiddleware middleware)
        {
            // The container is responsible for releasing resources.
        }
    }
}
