using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;

namespace _04_abp_MorningstarHandler
{
    public class MorningstarHandler : IDistributedEventHandler<StockCountChangedEto>, ITransientDependency
    {
        public Task HandleEventAsync(StockCountChangedEto eventData)
        {
            Console.WriteLine($"Morningstar handler stock count changed event {eventData}, thread id: {Thread.CurrentThread.ManagedThreadId}");
            return Task.CompletedTask;
        }
    }

    [EventName("App.Product.StockChange")] //EventName attribute is optional, but suggested. If you don't declare it, the event name will be the full name of the event class, _03_abp_LocalDistributedEventBus.StockCountChangedEto in this case.
    public class StockCountChangedEto
    {
        public Guid ProductId { get; set; }

        public int NewCount { get; set; }

        public override string ToString()
        {
            return $"ProductId: {ProductId}, new count: {NewCount}";
        }
    }
}
