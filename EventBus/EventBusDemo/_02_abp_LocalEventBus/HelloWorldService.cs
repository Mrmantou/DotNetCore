using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;

namespace _02_abp_LocalEventBus
{
    public class HelloWorldService : ITransientDependency
    {
        private readonly StockService stockService;

        public HelloWorldService(StockService stockService) => this.stockService = stockService;

        public async Task SayHello()
        {
            await stockService.ChangeStockCountAsync(Guid.NewGuid(), 100);


            Console.WriteLine("Hello World!");
        }
    }

    public class StockService : ITransientDependency
    {
        private readonly ILocalEventBus localEventBus;
        public StockService(ILocalEventBus localEventBus) => this.localEventBus = localEventBus;

        public async Task ChangeStockCountAsync(Guid productId, int newCount)
        {
            Console.WriteLine($"StockService publish stock count changed event -> ProductId: {productId}, new count: {newCount}, thread id: {Thread.CurrentThread.ManagedThreadId}");
            await localEventBus.PublishAsync(new StockCountChangedEvent { ProductId = productId, NewCount = newCount });
        }
    }

    public class MorningstarHandler : ILocalEventHandler<StockCountChangedEvent>, ITransientDependency
    {
        public Task HandleEventAsync(StockCountChangedEvent eventData)
        {
            Console.WriteLine($"Morningstar handler stock count changed event {eventData}, thread id: {Thread.CurrentThread.ManagedThreadId}");
            return Task.CompletedTask;
        }
    }

    public class MoodyHandler : ILocalEventHandler<StockCountChangedEvent>, ITransientDependency
    {
        public Task HandleEventAsync(StockCountChangedEvent eventData)
        {
            Console.WriteLine($"Moody handler stock count changed event {eventData}, thread id: {Thread.CurrentThread.ManagedThreadId}");
            return Task.CompletedTask;
        }
    }

    public class StockCountChangedEvent
    {
        public Guid ProductId { get; set; }

        public int NewCount { get; set; }

        public override string ToString()
        {
            return $"ProductId: {ProductId}, new count: {NewCount}";
        }
    }
}
