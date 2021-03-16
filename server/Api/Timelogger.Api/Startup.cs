using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Timelogger.Application.Commands.Projects;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Application;
using Timelogger.Lib.WebApi.ResponseWrapper.Extensions;
using Timelogger.Infrastructure.Data;
using Timelogger.Api.Filters;
using Timelogger.Domain.Abstractions;
using Timelogger.Api.Services;

namespace Timelogger.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            _environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ValidationFilter));
            }).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(typeof(CreateProjectCommandHandler).Assembly));
            services.Configure<ApiBehaviorOptions>(cfg => cfg.SuppressModelStateInvalidFilter = true);
            services.AddInfrastructure(Configuration.GetConnectionString("Default"));
            services.AddApplication();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddResponseWrapper(cfg =>
            {
                cfg.ApiVersion = "1.0";
                cfg.ShowApiVersion = true;
                cfg.IsDebug = false;
                cfg.IgnoreNullValue = true;
                cfg.EnableRequestLogging = true;
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Euroged CDN API", Version = "v1" });
            });
            if (_environment.IsDevelopment())
            {
                services.AddCors();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());
            }
            app.UseResponseWrapper();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Euroged CDN API");
                c.RoutePrefix = "swagger";
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllers();
            });


            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApiDbContext>().Database.Migrate();
            }
        }
    }
}