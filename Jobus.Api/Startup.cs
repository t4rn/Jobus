using Jobus.Api.Middleware;
using Jobus.Core.Repositories.WsClient;
using Jobus.Core.Services.Cache;
using Jobus.Core.Services.WsClients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Jobus.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IWsClientService, WsClientService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IWsClientRepository, WsClientRepository>();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Jobus API",
                    Description = "Jobus Swagger Documentation",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "admin", Url = "" },
                    License = new License { Name = "MIT", Url = "https://en.wikipedia.org/wiki/MIT_License" }
                });
            });

            RegisterTypes(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMiddleware<HashAuthorizationMiddleware>();
            app.UseMiddleware<LogRequestResponseMiddleware>();

            app.UseMvc();
        }

        private void RegisterTypes(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
