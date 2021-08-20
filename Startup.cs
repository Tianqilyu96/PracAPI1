using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSwag.AspNetCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc.Versioning;
using PracAPI1.Filters;
using PracAPI1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using PracAPI1.Services;
using PracAPI1.Infrastructure;

namespace PracAPI1
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
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddOpenApiDocument();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0); //add default version 1.0
                options.AssumeDefaultVersionWhenUnspecified = true; // if not found => 1.0
                options.ApiVersionReader = new MediaTypeApiVersionReader(); //specify where to find the api version info
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.ReportApiVersions = true;
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExeptionFilter>();
                options.Filters.Add<RequireHttpsFilter>();
            }); //add exception filter 
            services.AddCors(options => { options.AddPolicy("AllowApp", policy => policy.WithOrigins("https://somethingdiffrernt.com")); });
            //add CORS service
            services.Configure<HotelInfo>(Configuration.GetSection("Info")); //pull json  data out of configuration, and use it to populate an instance of HotelInfo
            //Use in-memory Database for quick dev and testing
            services.AddDbContext<HotelDbContext>(options => options.UseInMemoryDatabase("hoteldb"));
            services.AddScoped<IRoomService, RoomService>();
            //The entity framework core objects like the dbcontext,
            //used the scoped lifetime.
            //So any service that interacts with the dbcontext needs to be scoped as well. 
            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi(); // serve documents (same as app.UseSwagger())
                app.UseSwaggerUi3(); // serve Swagger UI

            }
            else
            {
                app.UseHsts();//prevent localhost using http error, only use https in production
            }

            //app.UseHttpsRedirection(); //  redirect http port to https port
            app.UseCors("AllowApp");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
