using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timelogger.Lib.WebApi.ResponseWrapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddResponseWrapper(this IServiceCollection services) => services.AddResponseWrapper(null);
        public static void AddResponseWrapper(this IServiceCollection services, Action<WrapperOptions> configureOptions)
        {
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }
            services.AddSingleton<ResponseOperations>();
        }

        public static void UseResponseWrapper(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseWrapperMiddleware>();
        }
    }
}
