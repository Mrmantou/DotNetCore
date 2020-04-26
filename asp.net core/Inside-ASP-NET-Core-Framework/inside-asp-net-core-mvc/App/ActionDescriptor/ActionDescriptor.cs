using System.Collections.Generic;

namespace App
{
    public abstract class ActionDescriptor
    {
        public AttributeRouteInfo AttributeRouteInfo { get; set; }
        public IDictionary<string, string> RouteValues { get; set; }
    }
}
