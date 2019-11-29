using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NLog.Extensions.Logging;

namespace Keim.NetCore.Extend
{
    public static class LoggingExtend
    {
        /// <summary>
        /// 初始化日志 测试为全输出，发布为错误等级
        /// </summary>
        /// <param name="webHost"></param>
        /// <returns></returns>
        public static IWebHostBuilder BuilderLoggin(this IWebHostBuilder webHost)
        {
            webHost.ConfigureLogging((hostingContext, logging) =>
              {
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
#if DEBUG
                  logging.SetMinimumLevel(LogLevel.Debug);
#else
                   logging.SetMinimumLevel(LogLevel.Error);
#endif
                  logging.AddDebug();
                  logging.AddConsole();
              });
            return webHost;
        }


        public static void BuilderLoggerFactory(this ILoggerFactory logger)
        {
            logger.AddNLog();
        }

        /// <summary>
        /// 初始化服务器端口
        /// </summary>
        /// <param name="webHost"></param>
        /// <param name="Port"></param>
        /// <param name="protocols"></param>
        /// <returns></returns>
        public static IWebHostBuilder BuilderKestrel(this IWebHostBuilder webHost,int Port=80, HttpProtocols protocols= HttpProtocols.Http1,string httpsFile="", string httpsPsd="")
        {
            webHost.ConfigureKestrel(options =>
              {
                  options.ListenAnyIP(Port, listenOptions =>
                  {
                      listenOptions.Protocols = protocols;
                      if(httpsFile.CheckIsNullOrEmpty() && httpsPsd.CheckIsNullOrEmpty())
                      {
                          listenOptions.UseHttps(httpsFile, httpsPsd);
                      }
                  });
              });

            return webHost;
        }
    }
}
