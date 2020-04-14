using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _ServiceHosting_05
{
    public class FakeHostedService : IHostedService
    {
        public FakeHostedService(IFoo foo, IBar bar, IBaz baz)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
            Debug.Assert(baz != null);
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
