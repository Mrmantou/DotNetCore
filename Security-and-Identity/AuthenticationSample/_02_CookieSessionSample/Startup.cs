using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace _02_CookieSessionSample
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            services.AddSingleton<ITicketStore, MemoryCacheTicketStore>();
            services.AddSingleton<MemoryCacheTicketStore>();

            services.AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
                .Configure<MemoryCacheTicketStore>((options, ticketStore) => options.SessionStore = ticketStore);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.Run(async context =>
            {
                if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
                {
                    var claims = new List<Claim>(1001);
                    claims.Add(new Claim(ClaimTypes.Name, "bob"));

                    for (int i = 0; i < 1000; i++)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "SomeRandomGroup" + i, ClaimValueTypes.String, "IssuedByBob", "OriginalIssuerJoe"));
                    }

                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Hello First timer");
                    return;
                }

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Hello old timer");
            });
        }
    }
}