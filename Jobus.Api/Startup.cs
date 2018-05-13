using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jobus.Api.Autofac;
using Jobus.Api.Middleware;
using Jobus.DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuthorizationFilter));
            });
            //services.AddMemoryCache(); <- indirectly added with services.AddMvc()

            // EF
            string connectionString = Configuration.GetConnectionString("cs");
            services.AddEntityFrameworkNpgsql().AddDbContext<JobusDbContext>(options => options.UseNpgsql(connectionString));

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

            // Autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AutofacModule());
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
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

            //app.UseMiddleware<HashAuthorizationMiddleware>();
            app.UseMiddleware<LogRequestResponseMiddleware>();

            app.UseMvc();
        }
    }
}
