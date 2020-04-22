using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace App
{
    public abstract class ActionEndpointDataSourceBase : EndpointDataSource
    {
        private readonly Lazy<IReadOnlyList<Endpoint>> endpointsAccessor;
        protected readonly List<Action<EndpointBuilder>> Conventions;

        public override IReadOnlyList<Endpoint> Endpoints => endpointsAccessor.Value;

        protected ActionEndpointDataSourceBase(IActionDescriptorCollectionProvider provider)
        {
            Conventions = new List<Action<EndpointBuilder>>();
            endpointsAccessor = new Lazy<IReadOnlyList<Endpoint>>(() => CreateEndpoints(provider.ActionDescriptors, Conventions));
        }

        public override IChangeToken GetChangeToken() => NullChangeToken.Instance;

        protected abstract List<Endpoint> CreateEndpoints(IReadOnlyList<ActionDescriptor> actions, IReadOnlyList<Action<EndpointBuilder>> conventions);

        private class NullChangeToken : IChangeToken
        {
            public bool ActiveChangeCallbacks => false;

            public bool HasChanged => false;

            public IDisposable RegisterChangeCallback(Action<object> callback, object state) => new NullDisposable();

            public static NullChangeToken Instance = new NullChangeToken();

            private class NullDisposable : IDisposable
            {
                public void Dispose() { }
            }
        }
    }
}
