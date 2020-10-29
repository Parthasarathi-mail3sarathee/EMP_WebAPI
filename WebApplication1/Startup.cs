using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using Unity;
using Unity.Lifetime;
using WebApplication_WebAPI.Filters;
using WebApplication_WebAPI.Middleware;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Service;
using WebApplication_Shared_Services.Model;
using WebApplication_Shared_Services.Contracts;

namespace WebApplication1
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAllHeaders", options => options.AllowAnyOrigin().AllowAnyHeader()
                           .AllowAnyMethod());
            });

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddScoped<IScopeService, ScopeService>(); 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddTransient<ILogHeaders, LogHeaders>();
            services.AddScoped<IBaseAuth, IdentityBasicAuthentication>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAuthService, AuthService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseLoggerMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader()
                           .AllowAnyMethod());
        }
    }
}
