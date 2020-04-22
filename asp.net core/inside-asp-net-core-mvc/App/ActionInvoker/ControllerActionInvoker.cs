using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public ActionContext ActionContext { get; }
        public ControllerActionInvoker(ActionContext actionContext) => ActionContext = actionContext;

        public Task InvokeAsync()
        {
            var actionDescriptor = ActionContext.ActionDescriptor as ControllerActionDescriptor;
            var controllerType = actionDescriptor.ControllerType;
            var requestService = ActionContext.HttpContext.RequestServices;
            var controllerInstance = ActivatorUtilities.CreateInstance(requestService, controllerType);
            if (controllerInstance is Controller controller)
            {
                controller.ActionContext = ActionContext;
            }
            var actionMethod = actionDescriptor.Method;
            var result = actionMethod.Invoke(controllerInstance, new object[0]);
            return result is Task task ? task : Task.CompletedTask;
        }
    }
}
