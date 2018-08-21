using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiddlewareExtensibilitySample.Data;
using MiddlewareExtensibilitySample.Middleware;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace MiddlewareExtensibilitySample
{
    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region******* my add code *******
            //使用 SimpleInjectorMiddlewareFactory 替换默认的中间件工厂
            services.AddTransient<IMiddlewareFactory>(_ =>
            {
                return new SimpleInjectorMiddlewareFactory(container);
            });

            //Wraps ASP.NET requests in an SimpleInjector.Lifestyles.AsyncScopedLifestyle.
            services.UseSimpleInjectorAspNetRequestScoping(container);

            services.AddScoped<AppDbContext>(provider => container.GetInstance<AppDbContext>());

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<AppDbContext>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
                optionsBuilder.UseInMemoryDatabase("InMemoryDb");
                return new AppDbContext(optionsBuilder.Options);
            }, Lifestyle.Scoped);

            //在容器中注册中间件
            container.Register<SimpleInjectorActivatedMiddleware>();

            container.Verify();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //在pipeline中添加中间件
            app.UseSimpleInjectorActivatedMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
