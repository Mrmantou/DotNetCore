using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ControllerActionEndpointConventionBuilder : IEndpointConventionBuilder
    {
        private readonly List<Action<EndpointBuilder>> conventions;

        public ControllerActionEndpointConventionBuilder(List<Action<EndpointBuilder>> conventions) => this.conventions = conventions;

        public void Add(Action<EndpointBuilder> convention) => conventions.Add(convention);

    }
}
