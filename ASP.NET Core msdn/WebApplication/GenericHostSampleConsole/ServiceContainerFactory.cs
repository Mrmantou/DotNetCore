using Microsoft.Extensions.DependencyInjection;
using System;

namespace GenericHostSampleConsole
{
    internal class ServiceContainerFactory : IServiceProviderFactory<ServiceContainer>
    {
        public ServiceContainer CreateBuilder(IServiceCollection services)
        {
            return new ServiceContainer();
        }

        public IServiceProvider CreateServiceProvider(ServiceContainer containerBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
