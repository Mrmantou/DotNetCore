using Albert.Demo.Application.Friends;
using Albert.Demo.Application.UrlNavs;
using Albert.Demo.EntityFramework.Sqlite;
using Albert.Domain.Repositories;
using Albert.Domain.Uow;
using Albert.EntityFrameworkCore.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Albert.Demo.Web
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
            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<DemoContext>(options => options.UseSqlite(Configuration.GetConnectionString("DemoDefault")));

            services.AddScoped<IUnitOfWork, UnitOfWork<DemoContext>>();

            services.AddTransient(typeof(IRepository<>), typeof(DemoRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(DemoRepository<,>));

            services.AddTransient<IFriendAppService, FriendAppService>();
            services.AddTransient<IUrlNavAppService, UrlNavAppService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Demo}/{action=Index}/{id?}");
            });
        }
    }
}
