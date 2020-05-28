using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_02_3
{
    public class MvcEngine
    {
        public IMvcEngineFactory EngineFactory { get; }
        public MvcEngine(IMvcEngineFactory engineFactory = null) => EngineFactory = engineFactory ?? new MvcEngineFactory();

        public async Task StartAsync(Uri address)
        {
            var listener = EngineFactory.GetWebListener();
            var activator = EngineFactory.GetControllerActivator();
            var executor = EngineFactory.GetControllerExecutor();
            var renderer = EngineFactory.GetViewRender();

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
