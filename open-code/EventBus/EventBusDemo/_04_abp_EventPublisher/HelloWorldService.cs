using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;

namespace _04_abp_EventPublisher
{
    public class HelloWorldService : ITransientDependency
    {
        private readonly StockService stockService;

        public HelloWorldService(StockService stockService) => this.stockService = stockService;

        public async Task SayHello()
        {
            string countStr;
            do
            {
                countStr = Console.ReadLine();

                if (!countStr.IsNullOrEmpty())
                {
                    int count = int.Parse(countStr);
                    await stockService.ChangeStockCountAsync(Guid.NewGuid(), count);
                }
            } while (!countStr.IsNullOrEmpty());

            Console.WriteLine("Hello World!");
        }
    }

    public class StockService : ITransientDependency
    {
        private readonly IDistributedEventBus distributedEventBus;
        public StockService(IDistributedEventBus distributedEventBus) => this.distributedEventBus = distributedEventBus;

        public async Task ChangeStockCountAsync(Guid productId, int newCount)
        {
            Console.WriteLine($"StockService publish stock count changed event -> ProductId: {productId}, new count: {newCount}, thread id: {Thread.CurrentThread.ManagedThreadId}");
            await distributedEventBus.PublishAsync(new StockCountChangedEto { ProductId = productId, NewCount = newCount });
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
