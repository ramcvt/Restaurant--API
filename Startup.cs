using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestaurantsAPI.Infrastructure;
using RestaurantsAPI.Models;
using RestaurantsAPI.Services;

namespace RestaurantsAPI
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
            //services.Configure<RestaurantDatabaseSettings>(Configuration.GetSection(nameof(RestaurantDatabaseSettings)));
            //services.AddSingleton<IRestaurantDatabaseSettings>(sp =>
            //    sp.GetRequiredService<IOptions<RestaurantDatabaseSettings>>().Value);
            ////services.AddScoped<RestaurantDBContext>();
            services.AddScoped<RestaurantService>();
            services.Configure<RestaurantDatabaseSettings>(Configuration.GetSection("RestaurantDatabaseSettings"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Event API",
                    Version = "V1",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Ram", Email = "janakiram.jb@gmail.com" }
                });
            });

            services.AddScoped<RestaurantDBContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors((c)=> {
                c.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event API");
                c.RoutePrefix = "";
            });

            app.UseMvc();
        }
    }
}
