﻿using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App
{
    /// <summary>
    /// 假设：所有的Controller类型都定义在当前ASP.NET Core应用所在的项目（程序集）中
    /// </summary>
    public class ControllerActionDescriptorProvider : IActionDescriptorProvider
    {
        private readonly Lazy<IEnumerable<ActionDescriptor>> accessor;
        public IEnumerable<ActionDescriptor> ActionDescriptors => accessor.Value;

        public ControllerActionDescriptorProvider(IHostEnvironment environment)
        {
            accessor = new Lazy<IEnumerable<ActionDescriptor>>(() => GetActionDescriptors(environment.ApplicationName));
        }

        private IEnumerable<ActionDescriptor> GetActionDescriptors(string applicationName)
        {
            var assemblyName = new AssemblyName(applicationName);
            var assembly = Assembly.Load(assemblyName);

            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.Name.EndsWith("Controller"))
                {
                    var controllerName = type.Name.Substring(0, type.Name.Length - "Controller".Length);
                    foreach (var method in type.GetMethods())
                    {
                        yield return CreateActionDescriptor(method, type, controllerName);
                    }
                }
            }
        }

        private ActionDescriptor CreateActionDescriptor(MethodInfo method, Type controllerType, string controllerName)
        {
            var actionName = method.Name;
            if (actionName.EndsWith("Async"))
            {
                actionName = actionName.Substring(0, actionName.Length - "Async".Length);
            }
            var templateProvider = method.GetCustomAttributes().OfType<IRouteTemplateProvider>().FirstOrDefault();

            if (templateProvider != null)
            {
                var routeInfo = new AttributeRouteInfo
                {
                    Order = templateProvider.Order ?? 0,
                    Template = templateProvider.Template
                };
                return new ControllerActionDescriptor
                {
                    AttributeRouteInfo = routeInfo,
                    ControllerType = controllerType,
                    Method = method
                };
            }

            return new ControllerActionDescriptor
            {
                ControllerType = controllerType,
                Method = method,
                RouteValues = new Dictionary<string, string>
                {
                    ["controller"] = controllerName,
                    ["action"] = actionName
                }
            };
        }
    }
}
