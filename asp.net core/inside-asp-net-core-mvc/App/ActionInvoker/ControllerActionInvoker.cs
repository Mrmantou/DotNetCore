using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App
{
    public class ControllerActionInvoker : IActionInvoker
    {
        private static readonly MethodInfo taskConvertMethod;
        private static readonly MethodInfo valueTaskConvertMethod;

        public ActionContext ActionContext { get; }
        public ControllerActionInvoker(ActionContext actionContext) => ActionContext = actionContext;

        static ControllerActionInvoker()
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;

            taskConvertMethod = typeof(ControllerActionInvoker).GetMethod(nameof(ConvertFromTaskAsync), bindingFlags);
            valueTaskConvertMethod = typeof(ControllerActionInvoker).GetMethod(nameof(ConvertFromValueTaskAsync), bindingFlags);
        }

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
            var returnValue = actionMethod.Invoke(controllerInstance, new object[0]);
            var mapper = requestService.GetRequiredService<IActionResultTypeMapper>();
            var actionResult = await ToActionResultAsync(returnValue, actionMethod.ReturnType, mapper);
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

        private Task<IActionResult> ToActionResultAsync(object returnValue, Type returnType, IActionResultTypeMapper mapper)
        {
            // Null
            if (returnValue == null || returnType == typeof(Task) || returnType == typeof(ValueTask))
            {
                return Task.FromResult<IActionResult>(NullActionResult.Instance);
            }

            // IActionResult
            if (returnValue is IActionResult actionResult)
            {
                return Task.FromResult(actionResult);
            }

            // Task<TResult>
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var declaredType = returnType.GenericTypeArguments.Single();
                var taskOfResult = taskConvertMethod.MakeGenericMethod(declaredType).Invoke(null, new object[] { returnValue, mapper });
                return (Task<IActionResult>)taskOfResult;
            }

            // ValueTask<TResult>
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
            {
                var declaredType = returnType.GenericTypeArguments.Single();
                var valueTaskOfResult = valueTaskConvertMethod.MakeGenericMethod(declaredType).Invoke(null, new object[] { returnValue, mapper });
                return (Task<IActionResult>)valueTaskOfResult;
            }

            return Task.FromResult(mapper.Convert(returnValue, returnType));
        }

        private static async Task<IActionResult> ConvertFromTaskAsync<TValue>(Task<TValue> returnValue, IActionResultTypeMapper mapper)
        {
            var result = await returnValue;
            return result is IActionResult actionResult ? actionResult : mapper.Convert(result, typeof(TValue));
        }

        private static async Task<IActionResult> ConvertFromValueTaskAsync<TValue>(ValueTask<TValue> returnValue, IActionResultTypeMapper mapper)
        {
            var result = await returnValue;
            return result is IActionResult actionResult ? actionResult : mapper.Convert(result, typeof(TValue));
        }
    }
}
