using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_02_2
{
    public class MvcEngine
    {
        public async Task StartAsync(Uri address)
        {
            var listener = GetWebListener();
            var activator = GetControllerActivator();
            var executor = GetControllerExecutor();
            var renderer = GetViewRender();

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

        protected virtual IWebListener GetWebListener()
        {
            throw new NotImplementedException();
        }

        protected virtual IControllerActivator GetControllerActivator()
        {
            throw new NotImplementedException();
        }

        protected virtual IControllerExecutor GetControllerExecutor()
        {
            throw new NotImplementedException();
        }

        protected virtual IViewRenderer GetViewRender()
        {
            throw new NotImplementedException();
        }
    }
}
