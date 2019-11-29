using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EnabledCorsExtend
    {
        public static IServiceCollection EnabledCors(this IServiceCollection vService, string Cors)
        {
            vService.AddCors(options =>
            {
                options.AddPolicy(Cors, builder =>
                 builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            });

            return vService;
        }
    }
}
