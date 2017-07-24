using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using myScheduleModels.Models;
using myScheduleModels.Models.Interfaces;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CommonDataAPI
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();
            env.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("RegisteredDataUser", p => p.RequireClaim("datauser", "True"));
            });

            services.AddMvc();
            services.AddTransient<IScheduleRepository, myScheduleRepo>();
            services.AddTransient<ILocationRepository, LocationRepo>();
            services.AddTransient<IListRepository, ListRepo>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var connection = _config.GetConnectionString("DefaultConnection");// @"Server=OFFICE_STAN\SQLEXPRESS;Database=mySchedule;Integrated Security=True;MultipleActiveResultSets=true";
            services.AddDbContext<myScheduleContext>(options => options.UseSqlServer(connection));
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddConsole(_config.GetSection("Logging"));
            // loggerFactory.AddDebug();
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()  // Microsoft.AspNetCore.Authentication.JwtBearer !!!!!
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _config["Tokens:Issuer"],
                    ValidAudience = _config["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])),
                    ValidateLifetime = true
                }
            });

            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
