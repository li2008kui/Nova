using Consul;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Nova.Utilities.Consul
{
    public class ConsulUtility
    {
        public static void ConsulService(string registerName, IApplicationLifetime lifetime)
        {
            string consulUrl = Environment.GetEnvironmentVariable("CONSUL_URL");
            string datacenter = Environment.GetEnvironmentVariable("DATACENTER");

            var client = new ConsulClient(c =>
            {
                c.Address = new Uri(consulUrl);
                c.Datacenter = datacenter;
            });

            string id = registerName + "-" + Guid.NewGuid();
            string serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL");
            Uri tmpUri = new Uri(serviceUrl);
            string address = tmpUri.Host;
            int port = tmpUri.Port;

            // 注册服务
            var result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = id,
                Name = registerName,
                Address = address,
                Port = port,
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"{serviceUrl}/api/HealthCheck",
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