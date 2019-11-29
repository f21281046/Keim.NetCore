using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UseSwaggerExtend
    {
        /// <summary>
        /// 启用Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        [Obsolete]
        public static IServiceCollection EnabledSwagger(this IServiceCollection services)
        {
           return services.AddSwaggerGen((s) =>
            {
                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = AppDomain.CurrentDomain.FriendlyName
                });
            });
        }

        /// <summary>
        /// 使用swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        [Obsolete]
        public static IApplicationBuilder EnabledSwagger(this IApplicationBuilder app)
        {
            try
            {

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs");
                    c.DocExpansion(DocExpansion.None);
                });
            }
            catch(Exception Erro)
            {

            }
            return app;
        }


     
    }
}
