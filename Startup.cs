﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ReportsSystemAPI.Infra;

namespace ReportsSystemAPI
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
            services.AddCors();

            services.AddDbContext<RSApiContext>(
                    opt => opt.UseNpgsql(
                        Configuration.GetConnectionString("conexaoPostgreSQL")
                    ));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                // c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports System - IMD/UFRN", Version = "v1" });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseCors(option => option.AllowAnyOrigin());
            // app.UseCors(option => option.AllowAnyMethod());
            // app.UseCors("AllowAnyOrigin");

            app.UseCors(
                options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();

        }
    }
}
