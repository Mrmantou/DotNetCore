using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;

namespace _04_abp_EventPublisher
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpEventBusRabbitMqModule)
    )]
    public class EventPublisherModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<EventPublisherHostedService>();

            //Configure<AbpRabbitMqEventBusOptions>(options =>
            //{
            //    options.ClientName = "MyClientName";
            //    options.ExchangeName = "MyExchangeName";
            //});

            //Configure<AbpRabbitMqOptions>(options => {
            //    options.Connections.Default.UserName = "admin";
            //    options.Connections.Default.Password = "admin";
            //    options.Connections.Default.HostName = "8.129.84.137";
            //    options.Connections.Default.Port = 5672;
            //});
        }
    }
}
