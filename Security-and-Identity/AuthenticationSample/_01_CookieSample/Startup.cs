using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Security.Claims;

namespace _01_CookieSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.Run(async context =>
            {
                if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
                {
                    ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "bob") }, CookieAuthenticationDefaults.AuthenticationScheme));
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Hello First time");
                    return;
                }

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Hello old time");
            });
        }
    }
}
