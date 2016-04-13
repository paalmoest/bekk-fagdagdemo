using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace EmployeeSvc
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.Use((context, next) =>
            {
                if (context.Request.Path.Equals("/healthy"))
                {
                    var data = JsonConvert.SerializeObject(new
                    {
                        status = "healthy",
                        environment = Environment.GetEnvironmentVariable("ENV") ?? "LOCAL"
                    });
                    return context.Response.WriteAsync(data);
                }
                return next.Invoke();
            });

            app.Use((context, next) =>
            {
                if (context.Request.Path.Equals("/ping-redis"))
                {
                    var client = new RedisClient("redis", 6379);
                    var ping = client.Ping();
                    return context.Response.WriteAsync(ping.ToString());
                }
                return next.Invoke();
            });
        }
    }
}