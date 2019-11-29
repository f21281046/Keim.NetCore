using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationExtend
    {
        /// <summary>
        /// 配置认证服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="clinetDTO"></param>
        public static void ConfigureIdentityServer(this IServiceCollection services, IdentityClinetDTO clinetDTO)
        {
            services.AddAuthentication((options) =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters();
             options.RequireHttpsMetadata = false;   //关闭HTTPS
             options.Audience = clinetDTO.Audience; //CLIENTID
             options.Authority = clinetDTO.EndPoint;    //地址
         });
        }
    }
}
