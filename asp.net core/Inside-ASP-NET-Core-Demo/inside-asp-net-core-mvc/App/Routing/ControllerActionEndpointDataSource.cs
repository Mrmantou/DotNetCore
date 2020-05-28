using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ControllerActionEndpointDataSource : ActionEndpointDataSourceBase
    {
        private readonly List<ConventionalRouteEntry> conventionalRoutes;
        private int order;
        private readonly RoutePatternTransformer routePatternTransformer;
        private readonly RequestDelegate requestDelegate;

        public ControllerActionEndpointConventionBuilder DefaultBuilder { get; }

        public ControllerActionEndpointDataSource(IActionDescriptorCollectionProvider provider, RoutePatternTransformer transformer) : base(provider)
        {
            conventionalRoutes = new List<ConventionalRouteEntry>();
            order = 0;
            routePatternTransformer = transformer;
            requestDelegate = ProcessRequestAsync;
            DefaultBuilder = new ControllerActionEndpointConventionBuilder(base.Conventions);
        }

        public ControllerActionEndpointConventionBuilder AddRoute(string routeName, string pattern, RouteValueDictionary defaults, IDictionary<string, object> constrains, RouteValueDictionary dataTokens)
        {
            List<Action<EndpointBuilder>> conventions = new List<Action<EndpointBuilder>>();
            order++;
            conventionalRoutes.Add(new ConventionalRouteEntry(routeName, pattern, defaults, constrains, dataTokens, order, conventions));
            return new ControllerActionEndpointConventionBuilder(conventions);
        }

        protected override List<Endpoint> CreateEndpoints(IReadOnlyList<ActionDescriptor> actions, IReadOnlyList<Action<EndpointBuilder>> conventions)
        {
            var endpoints = new List<Endpoint>();
            foreach (var action in actions)
            {
                var attributeInfo = action.AttributeRouteInfo;
                if (attributeInfo == null) //约定路由
                {
                    foreach (var route in conventionalRoutes)
                    {
                        var pattern = routePatternTransformer.SubstituteRequiredValues(route.Pattern, action.RouteValues);
                        if (pattern != null)
                        {
                            var builder = new RouteEndpointBuilder(requestDelegate, pattern, route.Order);
                            builder.Metadata.Add(action);
                            endpoints.Add(builder.Build());
                        }
                    }
                }
                else //特性路由
                {
                    var original = RoutePatternFactory.Parse(attributeInfo.Template);
                    var pattern = routePatternTransformer.SubstituteRequiredValues(original, action.RouteValues);
                    if (pattern != null)
                    {
                        var builder = new RouteEndpointBuilder(requestDelegate, pattern, attributeInfo.Order);
                        builder.Metadata.Add(action);
                        endpoints.Add(builder.Build());
                    }
                }
            }

            return endpoints;
        }

        private Task ProcessRequestAsync(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var actionDescriptor = endpoint.Metadata.GetMetadata<ActionDescriptor>();
            var actionContext = new ActionContext
            {
                ActionDescriptor = actionDescriptor,
                HttpContext = httpContext
            };

            var invokerFactory = httpContext.RequestServices.GetRequiredService<IActionInvokerFactory>();
            var invoker = invokerFactory.CreateInvoker(actionContext);
            return invoker.InvokeAsync();
        }
    }
}
