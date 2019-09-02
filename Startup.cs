using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ReportsSystemApi.Domain;
using ReportsSystemApi.Infra;

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

            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports System - IMD/UFRN", Version = "v1" });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                
            });
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
