using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace _Options_06
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string filePath, string basePath = null) where TOptions : class, new()
            => services.Configure<TOptions>(Options.DefaultName, filePath, basePath);

        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, string filePath, string basePath = null) where TOptions : class, new()
        {
            var fileProvider = string.IsNullOrEmpty(basePath)
                ? new PhysicalFileProvider(Directory.GetCurrentDirectory())
                : new PhysicalFileProvider(basePath);

            return services.AddSingleton<IConfigureOptions<TOptions>>(new JsonFileConfigureOptions<TOptions>(name, filePath, fileProvider));
        }

        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, TimeSpan refreshInterval) => services.AddSingleton<IOptionsChangeTokenSource<TOptions>>(new TimedRefreshTokenSource<TOptions>(refreshInterval, name));

        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, TimeSpan refreshInterval) => services.Configure<TOptions>(Options.DefaultName, refreshInterval);
    }
}
