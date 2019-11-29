using Consul;
using Keim.NetCore.DTO;
using Microsoft.AspNetCore.Builder.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Builder
{
    public static class RegisterConsulExtend
    {
        /// <summary>
        /// 注册Consul默认是80端口 如果健康为空则不检查
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <param name="serviceEntity">服务注册信息</param>
        /// <param name="HadlthPath">健康检查地址</param>
        /// <returns></returns>
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app,
           IApplicationLifetime lifetime,
           ServiceEntity serviceEntity, string HadlthPath)
        {
            try
            {
                if(serviceEntity.IsNull())
                {
                    throw new ArgumentNullException("ServiceEntity is Null");
                }

                if(serviceEntity.IsNetwork)
                {
                    var GetAddress = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                         .Select(p => p.GetIPProperties())
                         .SelectMany(p => p.UnicastAddresses)
                         .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
                         .FirstOrDefault()?.Address.ToString();
                    if (GetAddress != null)
                    {
                        serviceEntity.IP = GetAddress;
                        serviceEntity.Port = 80;
                    }
                }

                var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{serviceEntity.ConsulIP}:{serviceEntity.ConsulPort}"));//请求注册的 Consul 地址
                List<AgentServiceCheck> serviceChecks = new List<AgentServiceCheck>();
                if(!string.IsNullOrEmpty(HadlthPath))
                {
                    serviceChecks.Add(new AgentServiceCheck()
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(1),//服务启动多久后注册
                        Interval = TimeSpan.FromSeconds(15),//健康检查时间间隔，或者称为心跳间隔
                        HTTP = $"http://{serviceEntity.IP}:{serviceEntity.Port}{HadlthPath}",//健康检查地址
                        Timeout = TimeSpan.FromSeconds(5)
                    });
                }

                // Register service with consul
                var registration = new AgentServiceRegistration()
                {
                    Checks = serviceChecks.ToArray(),
                    ID = serviceEntity.ServiceName,
                    Name = serviceEntity.ServiceName,
                    Address = serviceEntity.IP,
                    Port = serviceEntity.Port,
                    Tags = new[] { $"urlprefix-/{serviceEntity.ServiceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
                };

                consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
                });
            }
            catch (Exception Erro)
            {
                Console.WriteLine(Erro.ToString());
            }
            return app;
        }

        /// <summary>
        /// 服务注册并返回
        /// </summary>
        /// <param name="service"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static void SingletonServiceEndPointCollection(this IServiceCollection service, IConfiguration Configuration)
        {
            using (var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{Configuration["Consul:IP"]}:{Configuration["Consul:Port"]}"); c.Datacenter = "dc1";
            }))
            {
                var ClientService = consulClient.Agent.Services().Result.Response;
                foreach (var Point in ClientService.Values)
                {
                    if (!MicroServiceManager.IsCheckService(Point.Service))
                    {
                        string GetClientService = $"http://{Point.Address}:{Point.Port}/";
                        Console.WriteLine(GetClientService);
                        MicroServiceManager.ServiceItems.Add(new MicroServiceItem()
                        {
                            ServiceTitle=Point.Service,
                            ServiceEndPoint= Point.Address,
                            ServicePort=Point.Port,
                        });
                    }
                }
            }

            service.AddSingleton(typeof(MicroServiceCollection), MicroServiceManager.ServiceItems);
        }

        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="service"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static List<MicroServiceItem> GetConsulServiceEndPointCollection(this IServiceCollection service, IConfiguration Configuration)
        {
            List<MicroServiceItem> serviceItems = new List<MicroServiceItem>();
            using (var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{Configuration["Consul:IP"]}:{Configuration["Consul:Port"]}"); c.Datacenter = "dc1";
            }))
            {
                var ClientService = consulClient.Agent.Services().Result.Response;
                foreach (var Point in ClientService.Values)
                {
                    if (!MicroServiceManager.IsCheckService(Point.Service))
                    {
                        string GetClientService = $"http://{Point.Address}:{Point.Port}/";
                        Console.WriteLine(GetClientService);
                        serviceItems.Add(new MicroServiceItem()
                        {
                            ServiceTitle = Point.Service,
                            ServiceEndPoint = Point.Address,
                            ServicePort = Point.Port,
                        });
                    }
                }
            }

            return serviceItems;
        }
    }
}
