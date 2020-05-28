using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_02_3
{
    public class FoobarEngineFactory : MvcEngineFactory
    {
        public override IControllerActivator GetControllerActivator() => new SingletonControllerActivator();
    }

    public class SingletonControllerActivator : IControllerActivator
    {
        public Task<Controller> CreateControllerAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public Task ReleaseAsync(Controller controller) => Task.CompletedTask;
    }
}
