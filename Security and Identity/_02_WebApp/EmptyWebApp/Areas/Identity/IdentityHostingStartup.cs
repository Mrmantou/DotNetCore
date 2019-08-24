using System;
using EmptyWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EmptyWebApp.Areas.Identity.IdentityHostingStartup))]
namespace EmptyWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EmptyWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EmptyWebAppContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<EmptyWebAppContext>();
            });
        }
    }
}