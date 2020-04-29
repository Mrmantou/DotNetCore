using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App
{
    public class DynamicActionProvider : IActionDescriptorProvider
    {
        private readonly List<ControllerActionDescriptor> actions;
        private readonly Func<string, IEnumerable<ControllerActionDescriptor>> creator;

        public DynamicActionProvider(IServiceProvider serviceProvider, ICompiler compiler)
        {
            actions = new List<ControllerActionDescriptor>();
            creator = CreateActionDescrptors;

            IEnumerable<ControllerActionDescriptor> CreateActionDescrptors(string sourceCode)
            {
                var assembly = compiler.Compile(sourceCode,
                    Assembly.Load(new AssemblyName("System.Runtime")),
                    typeof(object).Assembly,
                    typeof(ControllerBase).Assembly,
                    typeof(Controller).Assembly);

                var controllerTypes = assembly.GetTypes().Where(i => IsController(i));
                var applicationModel = CreateApplicationModel(controllerTypes);

                assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                var typeName = "Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorBuilder";
                var controllerBuilderType = assembly.GetTypes().Single(i => i.FullName == typeName);
                var buildMethod = controllerBuilderType.GetMethod("Build", BindingFlags.Static | BindingFlags.Public);
                return (IEnumerable<ControllerActionDescriptor>)buildMethod.Invoke(null, new object[] { applicationModel });
            }

            ApplicationModel CreateApplicationModel(IEnumerable<Type> controllerTypes)
            {
                var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                var typeName = "Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModelFactory";
                var factoryType = assembly.GetTypes().Single(i => i.FullName == typeName);
                var factory = serviceProvider.GetService(factoryType);
                var method = factoryType.GetMethod("CreateApplicationModel");
                var typeInfos = controllerTypes.Select(i => i.GetTypeInfo());
                return (ApplicationModel)method.Invoke(factory, new object[] { typeInfos });
            }

            bool IsController(Type typeInfo)
            {
                if (!typeInfo.IsClass
                    || typeInfo.IsAbstract
                    || !typeInfo.IsPublic
                    || typeInfo.ContainsGenericParameters
                    || typeInfo.IsDefined(typeof(NonControllerAttribute))
                    || !typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                            && !typeInfo.IsDefined(typeof(ControllerAttribute)))
                {
                    return false;
                }
                return true;
            }
        }


        public int Order => -100;

        public void OnProvidersExecuted(ActionDescriptorProviderContext context) { }

        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            foreach (var action in actions)
            {
                context.Results.Add(action);
            }
        }

        public void AddControllers(string sourceCode) => actions.AddRange(creator(sourceCode));
    }
}
