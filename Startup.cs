using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ApiTienda.Data;
using ApiTienda.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TiendaAPI.Services;
using System.Text;

namespace ApiTienda
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
            //DbContext
            services.AddDbContext<TiendaBDContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TiendaConnection")));

            //Servie layer
            services.AddScoped<ProductoService>();
            services.AddScoped<OfertaService>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<AuthService>();
            
            //cords
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins", policy => {
                        policy.WithOrigins("*");
                        policy.AllowAnyHeader();
                    policy.WithMethods("GET", "POST", "PUT", "DELETE");
                    });
            });

            //jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
                };
            });

            //otros
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                //autorizacion desde swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Autorizacion.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiTienda", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTienda v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            //cords
            app.UseCors("_myAllowSpecificOrigins");

            //Authentication
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
