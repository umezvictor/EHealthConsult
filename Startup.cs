using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using AutoMapper;
using EHealthConsult.Models;
using EHealthConsult.Repository.Interface;
using EHealthConsult.Repository.RepositoryClasses;
using EHealthConsult.Services;
using EHealthConsult.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EHealthConsult
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

            /rate limiting
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //load ip rules from appsettings.json
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // Add framework services.
            services.AddMvc();

            // https://github.com/aspnet/Hosting/issues/793
            // the IHttpContextAccessor service is not registered by default.
            // the clientId/clientIp resolvers use it.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            //end of rate limiting
            


            services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            //identity
            //custom class, AppUser is used in place of IdentityUser, cos it extends IdentityUser
            services.AddIdentity<AppUser, IdentityRole>(options => {
                //password configuration
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;

            }).AddEntityFrameworkStores<AppDbContext>();

            //repository wrapper -- handles all crud functionality
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            //email service
            services.AddScoped<IEmailService, EmailService>();

            //automapper
            services.AddAutoMapper(typeof(Startup));

            // configuring jwt as the authentication mechanism
            services.AddAuthentication(opt => {
                //authentication scheme
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //challenge scheme
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                         .AddJwtBearer(options =>
                         {
                             options.TokenValidationParameters = new TokenValidationParameters
                             {
                                 ValidateIssuer = true,
                                 ValidateAudience = true,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,

                                 ValidIssuer = Configuration.GetSection("JwtSettings:Issuer").Value,
                                 ValidAudience = Configuration.GetSection("JwtSettings:Audience").Value,
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSettings:JwtSecret").Value))
                             };
                         });

         //cors
            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // The Stackify middleware needs to come before any exception handling middleware and the MVC middleware
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
            //app.UseMiddleware<SerilogRequestLogger>();
          
           
            //rate limiting middleware
           // app.UseIpRateLimiting();
            //cors
            app.UseCors(options => 
            options.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
