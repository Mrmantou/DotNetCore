using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace _02_abp_LocalEventBus
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpEventBusModule)
    )]
    public class LocalEventBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<LocalEventBusHostedService>();
        }
    }
}
