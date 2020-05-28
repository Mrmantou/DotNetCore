using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_03
{
    public class MvcEngine
    {
        public Cat Cat { get; }
        public MvcEngine(Cat cat) => Cat = cat;

        public async Task StartAsync(Uri address)
        {
            var listener = Cat.GetService<IWebListener>();//Service Locator
            var activator = Cat.GetService<IControllerActivator>();
            var executor = Cat.GetService<IControllerExecutor>();
            var renderer = Cat.GetService<IViewRenderer>();

            await listener.ListenAsync(address);

            while (true)
            {
                var httpContext = await listener.ReceiveAsync();
                var controller = await activator.CreateControllerAsync(httpContext);

                try
                {
                    var view = await executor.ExecuteAsync(controller, httpContext);
                    await renderer.RenderAsync(view, httpContext);
                }
                finally
                {
                    await activator.ReleaseAsync(controller);
                }
            }
        }
    }
}
