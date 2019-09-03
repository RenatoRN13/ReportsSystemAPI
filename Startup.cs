using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReportsSystemApi.Domain;
using ReportsSystemApi.Infra;
using Swashbuckle.AspNetCore.Swagger;

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

            var signingConfig = new SigningConfig();
            services.AddSingleton(signingConfig);

            var tokenConfig = new TokenConfig();
            new ConfigureFromConfigurationOptions<TokenConfig>(Configuration.GetSection("TokenConfigs")).Configure(tokenConfig);
            services.AddSingleton(tokenConfig);
            
            services.AddAuthentication (authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (bearerOptions => {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfig.key;
                paramsValidation.ValidAudience = tokenConfig.audience;
                paramsValidation.ValidIssuer = tokenConfig.issuer;

            // Validade a assinatura do token
            paramsValidation.ValidateIssuerSigningKey = true;
            // Verifica validade do token
            paramsValidation.ValidateLifetime = true;

            services.AddAuthorization (auth => {
                auth.AddPolicy (
                    "Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder ()
                    .AddAuthenticationSchemes (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser ().Build ()
                );
            });

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports System - IMD/UFRN", Version = "v1" });
            // });
            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new Info { Title = "Sistema de Relatórios - IMD/UFRN", Version = "v1" });
                    var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseCors(option => option.AllowAnyOrigin());
            // app.UseCors(option => option.AllowAnyMethod());
            // app.UseCors("AllowAnyOrigin");

            app.UseCors(
                options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
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
