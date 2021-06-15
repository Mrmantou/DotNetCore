using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_MediatR
{
    public class Pinged : INotification
    {

    }

    public class Ponged : INotification
    {

    }

    public class PingedHandler : INotificationHandler<Pinged>
    {
        public Task Handle(Pinged notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PingedHandler thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Got pinged async.");
            return Task.CompletedTask;
        }
    }

    public class PongedHandler : INotificationHandler<Ponged>
    {
        public Task Handle(Ponged notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PongedHandler thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Got ponged async.");
            return Task.CompletedTask;
        }
    }

    public class ConstrainedPingedHandler<TNotification> : INotificationHandler<TNotification>
        where TNotification : Pinged
    {
        public Task Handle(TNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"ConstrainedPingedHandler thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Got pinged constrained async.");
            return Task.CompletedTask;
        }
    }

    public class PingedAlsoHandler : INotificationHandler<Pinged>
    {
        public Task Handle(Pinged notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PingedAlsoHandler thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Got pinged also async.");
            return Task.CompletedTask;
        }
    }
}
