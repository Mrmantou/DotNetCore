using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcControllers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IActionDescriptorCollectionProvider, DefaultActionDescriptorCollectionProvider>()
                .AddSingleton<IActionInvokerFactory, ActionInvokerFactory>()
                .AddSingleton<IActionDescriptorProvider, ControllerActionDescriptorProvider>()
                .AddSingleton<ControllerActionEndpointDataSource, ControllerActionEndpointDataSource>()
                .AddSingleton<IActionResultTypeMapper, ActionResultTypeMapper>()

                .AddSingleton<IValueProviderFactory, HttpHeaderValueProviderFactory>()
                .AddSingleton<IValueProviderFactory, QueryStringValueProviderFactory>()
                .AddSingleton<IValueProviderFactory, FormValueProviderFactory>()
                .AddSingleton<IModelBinderFactory, ModelBinderFactory>()
                .AddSingleton<IModelBinderProvider, SimpleTypeModelBinderProvider>()
                .AddSingleton<IModelBinderProvider, ComplexTypeModelBinderProvider>()
                .AddSingleton<IModelBinderProvider, BodyModelBinderProvider>();
        }
    }
}
