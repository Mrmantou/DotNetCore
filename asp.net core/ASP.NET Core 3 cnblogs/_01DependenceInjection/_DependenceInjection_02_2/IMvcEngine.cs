using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_02_2
{
    public interface IWebListener
    {
        Task ListenAsync(Uri address);
        Task<HttpContext> ReceiveAsync();
    }

    public interface IControllerActivator
    {
        Task<Controller> CreateControllerAsync(HttpContext httpContext);
        Task ReleaseAsync(Controller controller);
    }

    public interface IControllerExecutor
    {
        Task<View> ExecuteAsync(Controller controller, HttpContext httpContext);
    }

    public interface IViewRenderer
    {
        Task RenderAsync(View view, HttpContext httpContext);
    }

    public class HttpContext
    {
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
