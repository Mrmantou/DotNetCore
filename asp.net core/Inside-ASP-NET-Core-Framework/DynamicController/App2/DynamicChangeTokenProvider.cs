using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public class DynamicChangeTokenProvider : IActionDescriptorChangeProvider
    {
        private CancellationTokenSource source;
        private CancellationChangeToken token;

        public DynamicChangeTokenProvider()
        {
            source = new CancellationTokenSource();
            token = new CancellationChangeToken(source.Token);
        }

        public IChangeToken GetChangeToken() => token;

        public void NotifyChanges()
        {
            var old = Interlocked.Exchange(ref source, new CancellationTokenSource());
            token = new CancellationChangeToken(source.Token);
            old.Cancel();
        }
    }
}
