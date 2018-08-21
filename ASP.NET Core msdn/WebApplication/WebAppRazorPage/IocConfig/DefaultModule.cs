using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppRazorPage.DependencyInjection;

namespace WebAppRazorPage.IocConfig
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyDependency>().As<IMyDependency>();
        }
    }
}
