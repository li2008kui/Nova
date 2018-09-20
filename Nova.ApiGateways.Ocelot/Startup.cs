using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;

namespace Nova.ApiGateways.Ocelot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string authBaseAddress = Environment.GetEnvironmentVariable("AUTH_BASE_ADDRESS");
            services.AddAuthentication()
                .AddIdentityServerAuthentication("TicketKey", o =>
                {
                    o.Authority = authBaseAddress;
                    o.ApiName = "Ticket.API";
                    //o.ApiSecret = "xxx";
                    o.RequireHttpsMetadata = false;
                    o.SupportedTokens = SupportedTokens.Both;
                    o.JwtValidationClockSkew = TimeSpan.FromSeconds(1000);//Token过期时间偏移
                });
            services.AddOcelot(Configuration)
                .AddConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseOcelot().Wait();
        }
    }
}
