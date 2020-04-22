using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public static class EndpointRouteBuilderExtensions
    {
        public static ControllerActionEndpointConventionBuilder MapMvcControllers(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var endpointDataSource = endpointRouteBuilder.ServiceProvider.GetRequiredService<ControllerActionEndpointDataSource>();
            endpointRouteBuilder.DataSources.Add(endpointDataSource);
            return endpointDataSource.DefaultBuilder;
        }

        public static ControllerActionEndpointConventionBuilder MapMvcControllerRoute(this IEndpointRouteBuilder endpointRouteBuilder, string name, string pattern, RouteValueDictionary defaults = null, RouteValueDictionary contrains = null, RouteValueDictionary dataTokens = null)
        {
            var endpointDataSource = endpointRouteBuilder.ServiceProvider.GetRequiredService<ControllerActionEndpointDataSource>();
            endpointRouteBuilder.DataSources.Add(endpointDataSource);
            return endpointDataSource.AddRoute(name, pattern, defaults, contrains, dataTokens);
        }
    }
}
