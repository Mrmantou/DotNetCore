using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_MediatR
{
    public class Ping : IRequest<string> { }

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PingHandler thread id: {Thread.CurrentThread.ManagedThreadId}");
            return Task.FromResult("Pong");
        }
    }

    public class OneWay : IRequest { }
    public class OneWayHandlerWithBaseClass : AsyncRequestHandler<OneWay>
    {
        protected override Task Handle(OneWay request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"OneWayHandlerWithBaseClass thread id: {Thread.CurrentThread.ManagedThreadId}");

            return Task.CompletedTask;
        }
    }

    public class SyncPing : IRequest<string> { }
    /// <summary>
    /// synchronous
    /// </summary>
    public class SyncHandler : RequestHandler<SyncPing, string>
    {
        protected override string Handle(SyncPing request)
        {
            return "Pong";
        }
    }
}
