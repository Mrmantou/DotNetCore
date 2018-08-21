using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebMiddleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Map supports nesting, for example:
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Map
            app.Map("/map1", HandleMapTest1);
            app.Map("/map2", HandleMapTest2);

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);

            //app.Map("/level1", level1App =>
            //{
            //    level1App.Map("/level2a", level2AApp =>
            //    {
            //        // "/level1/level2a"
            //        //...
            //        app.Run(async context =>
            //        {
            //            await context.Response.WriteAsync("/level1/level2a");
            //        });
            //    });
            //    level1App.Map("/level2b", level2BApp =>
            //    {
            //        // "/level1/level2b"
            //        //...
            //        app.Run(async context =>
            //        {
            //            await context.Response.WriteAsync("/level1/level2b");
            //        });
            //    });
            //});

            //app.Map("/level1/level2a", app1 =>
            //{
            //    app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("/level1/level2a");
            //    });
            //});

            //app.Map("/level1/level2b", app1 =>
            //{
            //    app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("/level1/level2b");
            //    });
            //});
            #endregion

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
            //});

            app.Use((context, next) =>
            {
                var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {
                    var culture = new CultureInfo(cultureQuery);

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }

                // Call the next delegate/middleware in the pipeline
                return next();
            });

            // app.UseRequestCulture();//调用扩展方法添加RequestCulture中间件

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello {CultureInfo.CurrentCulture.DisplayName}");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used={branchVer}");
            });
        }

        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }
    }
}
