﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProCoding.Demos.ASPNetCore.Cache
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            // // dotnet sql-cache create "Data Source=.;User Id=sa;Password=P@$$w0rd;Initial Catalog=CacheDb;" dbo ASPNetCoreCache
            // services.AddDistributedSqlServerCache(options =>
            // {
            //     options.ConndotectionString = Configuration.GetConnectionString("cacheLocalSqlServer");
            //     options.SchemaName = "dbo";
            //     options.TableName = "ASPNetCoreCache";
            // });

            // // dotnet add package Microsoft.Extensions.Caching.Redis
            // services.AddDistributedRedisCache(options =>
            // {
            //     // options.Configuration = Configuration.GetConnectionString("cacheAzureRedis");
            //     options.Configuration = Configuration.GetConnectionString("cacheLocalRedis");
            //     options.InstanceName = "CacheInstance";
            // });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
