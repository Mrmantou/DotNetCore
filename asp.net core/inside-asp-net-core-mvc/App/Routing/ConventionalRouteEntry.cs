using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ConventionalRouteEntry
    {
        public string RouteName;
        public RoutePattern Pattern { get; }
        public RouteValueDictionary DataTokens { get; }
        public int Order { get; }
        public IReadOnlyList<Action<EndpointBuilder>> Conventions { get; }

        public ConventionalRouteEntry(string routeName, string pattern, RouteValueDictionary defaults, IDictionary<string, object> contraints, RouteValueDictionary dataTokens, int order, List<Action<EndpointBuilder>> conventions)
        {
            RouteName = routeName;
            DataTokens = dataTokens;
            Order = order;
            Conventions = conventions;
            Pattern = RoutePatternFactory.Parse(pattern, defaults, contraints);
        }
    }
}
