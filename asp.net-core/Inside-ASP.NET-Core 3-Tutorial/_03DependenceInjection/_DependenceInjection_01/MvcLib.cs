using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_01
{
    public static class MvcLib
    {
        public static Task ListenAsync(Uri address)
        {
            throw new NotImplementedException();
        }

        public static Task<Request> ReceiveAsync()
        {
            throw new NotImplementedException();
        }

        public static Task<Controller> CreateControllerAsync(Request request)
        {
            throw new NotImplementedException();
        }

        public static Task<View> ExecuteControllerAsync(Controller controller)
        {
            throw new NotImplementedException();
        }
        public static Task RenderViewAsync(View view)
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
