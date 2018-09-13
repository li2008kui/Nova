using Consul;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Nova.Services.Ids4
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
            string ids4Url = Environment.GetEnvironmentVariable("IDS4_URL");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddIdentityServer(o => o.IssuerUri = ids4Url)
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                })
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseIdentityServer();

            string consulUrl = Environment.GetEnvironmentVariable("CONSUL_URL");
            string datacenter = Environment.GetEnvironmentVariable("DATACENTER");
            ConsulService(consulUrl, datacenter, lifetime);
        }

        private void ConsulService(string consulUrl, string datacenter, IApplicationLifetime lifetime)
        {
            var client = new ConsulClient(c =>
            {
                c.Address = new Uri(consulUrl);
                c.Datacenter = datacenter;
            });

            string name = "Nova-Services-Ids4";
            string id = name + "-" + Guid.NewGuid();

            string ids4Url = Environment.GetEnvironmentVariable("IDS4_URL");
            Uri tmpUri = new Uri(ids4Url);
            string address = tmpUri.Host;
            int port = tmpUri.Port;

            // 注册服务
            var result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = id,
                Name = name,
                Address = address,
                Port = port,
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"{ids4Url}/api/HealthCheck",
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
            result.Wait();

            // 注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister(id).Wait();
            });
        }
    }
}