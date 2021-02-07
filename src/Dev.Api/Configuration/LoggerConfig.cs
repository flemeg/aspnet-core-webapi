using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elmah.Io.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Dev.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {

            var appkey = configuration.GetSection("ElmahIoSettings").GetValue<string>("AppKey");
            var logId = configuration.GetSection("ElmahIoSettings").GetValue<Guid>("LogId");

            services.AddElmahIo(o =>
            {
                o.ApiKey = appkey;
                o.LogId = logId;
            });

            services.AddLogging(builder =>
            {
                builder.AddElmahIo(o =>
                {
                    o.ApiKey = appkey;
                    o.LogId = logId;
                });
                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            });

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();
            return app;
        }
    }
}
