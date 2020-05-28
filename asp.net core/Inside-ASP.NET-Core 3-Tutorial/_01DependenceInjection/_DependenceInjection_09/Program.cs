using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace _DependenceInjection_09
{
    /// <summary>
    /// <!--依赖注入框架总结出如下的特性：-->
    /// <!--1-->ServiceProviderEngine的唯一性：整个服务提供体系只存在一个唯一的ServiceProviderEngine对象。
    /// <!--2-->ServiceProviderEngine与IServiceFactory的同一性：唯一存在的ServiceProviderEngine会作为创建服务范围的IServiceFactory工厂。
    /// <!--3-->ServiceProviderEngineScope和IServiceProvider的同一性：表示服务范围的ServiceProviderEngineScope同时也是作为服务提供者的依赖注入容器。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProviderTest();

            RegistIServiceProviderTest();
        }

        private static void RegistIServiceProviderTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<SingletonService>()
            .AddScoped<ScopedService>()
            .BuildServiceProvider();
            var rootScope = serviceProvider.GetService<IServiceProvider>();
            using (var scope = serviceProvider.CreateScope())
            {
                var child = scope.ServiceProvider;
                var singletonService = child.GetRequiredService<SingletonService>();
                var scopedService = child.GetRequiredService<ScopedService>();

                Debug.Assert(ReferenceEquals(child, scopedService.RequestServices));
                Debug.Assert(ReferenceEquals(rootScope, singletonService.ApplicationServices));
            }
        }

        private static void RegistIServiceProviderTest1()
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<SingletonService>()
            .AddScoped<ScopedService>()
            .BuildServiceProvider();
            var rootScope = serviceProvider.GetService<IServiceProvider>();
            using (var scope = serviceProvider.CreateScope())
            {
                var child = scope.ServiceProvider;
                var singletonService = child.GetRequiredService<SingletonService>();
                var scopedService = child.GetRequiredService<ScopedService>();

                Debug.Assert(ReferenceEquals(child, child.GetRequiredService<IServiceProvider>()));
                Debug.Assert(ReferenceEquals(child, scopedService.RequestServices));
                Debug.Assert(ReferenceEquals(rootScope, singletonService.ApplicationServices));
            }
        }

        private static void ServiceProviderTest()
        {
            var (engineType, engineScopeType) = ResolveTypes();

            var root = new ServiceCollection().BuildServiceProvider();

            var child1 = root.CreateScope().ServiceProvider;
            var child2 = root.CreateScope().ServiceProvider;

            var engine = GetEngine(root);
            var rootScope = GetRootScope(engine, engineType);

            //ServiceProviderEngine的唯一性
            Debug.Assert(ReferenceEquals(GetEngine(rootScope, engineScopeType), engine));
            Debug.Assert(ReferenceEquals(GetEngine(child1, engineScopeType), engine));
            Debug.Assert(ReferenceEquals(GetEngine(child2, engineScopeType), engine));

            //ServiceProviderEngine和IServiceScopeFactory的同一性
            Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceScopeFactory>(), engine));
            Debug.Assert(ReferenceEquals(child1.GetRequiredService<IServiceScopeFactory>(), engine));
            Debug.Assert(ReferenceEquals(child2.GetRequiredService<IServiceScopeFactory>(), engine));

            //ServiceProviderEngineScope提供的IServiceProvider是它自己
            //ServiceProvider提供的IServiceProvider是RootScope
            Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceProvider>(), rootScope));
            Debug.Assert(ReferenceEquals(child1.GetRequiredService<IServiceProvider>(), child1));
            Debug.Assert(ReferenceEquals(child2.GetRequiredService<IServiceProvider>(), child2));

            //ServiceProviderEngineScope和IServiceProvider的同一性
            Debug.Assert(ReferenceEquals((rootScope).ServiceProvider, rootScope));
            Debug.Assert(ReferenceEquals(((IServiceScope)child1).ServiceProvider, child1));
            Debug.Assert(ReferenceEquals(((IServiceScope)child2).ServiceProvider, child2));
        }

        static (Type engineType, Type engineScopeType) ResolveTypes()
        {
            var assembly = typeof(ServiceProvider).Assembly;
            var engine = assembly.GetTypes().Single(item => item.Name == "IServiceProviderEngine");
            var engineScope = assembly.GetTypes().Single(item => item.Name == "ServiceProviderEngineScope");
            return (engine, engineScope);
        }

        static object GetEngine(ServiceProvider serviceProvider)
        {
            var field = typeof(ServiceProvider).GetField("_engine", BindingFlags.Instance | BindingFlags.NonPublic);

            return field.GetValue(serviceProvider);
        }

        static object GetEngine(object engineScope, Type engineScopeType)
        {
            var property = engineScopeType.GetProperty("Engine", BindingFlags.Instance | BindingFlags.Public);
            return property.GetValue(engineScope);
        }

        static IServiceScope GetRootScope(object engine, Type engineType)
        {
            var property = engineType.GetProperty("RootScope", BindingFlags.Instance | BindingFlags.Public);
            return (IServiceScope)property.GetValue(engine);
        }
    }

    public class SingletonService
    {
        public IServiceProvider ApplicationServices { get; }
        public SingletonService(IServiceProvider serviceProvider) => ApplicationServices = serviceProvider;
    }

    public class ScopedService
    {
        public IServiceProvider RequestServices { get; }
        public ScopedService(IServiceProvider serviceProvider) => RequestServices = serviceProvider;
    }
}
