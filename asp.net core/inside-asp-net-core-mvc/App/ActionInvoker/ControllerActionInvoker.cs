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

        public async Task InvokeAsync()
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
            var actionResult = await ToActionResultAsync(result);
            await actionResult.ExecuteResultAsync(ActionContext);
        }

        private async Task<IActionResult> ToActionResultAsync(object result)
        {
            if (result == null)
            {
                return NullActionResult.Instance;
            }

            if (result is Task<IActionResult> taskOfActionResult)
            {
                return await taskOfActionResult;
            }

            if (result is ValueTask<IActionResult> valueTaskOfActionResult)
            {
                return await valueTaskOfActionResult;
            }

            if (result is IActionResult actionResult)
            {
                return actionResult;
            }

            if (result is Task task)
            {
                await task;
                return NullActionResult.Instance;
            }

            throw new InvalidOperationException("Action method's return value is invalid.");
        }
    }
}
