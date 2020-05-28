using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _Start_03_MVC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services
            .AddRouting()
            .AddControllersWithViews();

        public void Configure(IApplicationBuilder app) => app
            .UseRouting()
            .UseEndpoints(endpointers => endpointers.MapControllers());
    }
}
