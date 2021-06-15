using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace _03_abp_LocalDistributedEventBus
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpEventBusModule)
    )]
    public class LocalDistributedEventBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<LocalDistributedEventBusHostedService>();
        }
    }
}
