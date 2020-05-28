using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_02_1
{
    public class MvcEngine
    {
        public async Task StartAsync(Uri address)
        {
            await ListenAsync(address);

            while (true)
            {
                var request = await ReceiveAsync();
                var controller = await CreateControllerAsync(request);
                var view = await ExecuteControllerAsync(controller);
                await RenderViewAsync(view);
            }
        }

        protected virtual Task ListenAsync(Uri address)
        {
            throw new NotImplementedException();
        }

        protected virtual Task<Request> ReceiveAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual Task<Controller> CreateControllerAsync(Request request)
        {
            throw new NotImplementedException();
        }

        protected virtual Task<View> ExecuteControllerAsync(Controller controller)
        {
            throw new NotImplementedException();
        }

        protected virtual Task RenderViewAsync(View view)
        {
            throw new NotImplementedException();
        }
    }

    public class View
    {
    }

    public class Controller
    {
    }

    public class Request
    {
    }
}
