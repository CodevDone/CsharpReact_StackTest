using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOrc.Try.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DevOrc.Try.BLL.Interfaces;
using DevOrc.Try.DAL.Interfaces;
using DevOrc.Try.DAL.Repositories;
using DevOrc.Try.BLL.Services;
// using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using DevOrc.Try.DAL.Model;

namespace DevOrc.Try.API
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

            services.AddControllers();
            // Agregar Swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevOrc.Try.API", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
                // c.SwaggerDoc("DevOrc.Try.API", new OpenApiInfo {Title = "DevOrc.Try.API", Version = "1.0", Description = "my api " });
                // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });



            // // Agregar Cors Config
            // services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //  {
            //      builder
            //      .AllowAnyOrigin()
            //      //.WithOrigins("https://localhost", "http://localhost:3000")
            //      .AllowAnyHeader()
            //      .AllowAnyMethod()
            //      ;
            //  }));

            // Agregar Db Context
            // services.AddDbContext<TodoContext>(opt => opt.UseSqlite(Configuration["ConnectionsString"]));
            services.AddDbContext<todoContext>(opt => opt.UseSqlServer(Configuration["ConnectionsString"]));
            // Agregar Servicios Scoped
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseCors("MyPolicy");
            app.UseCors(options =>
            {
                options.WithOrigins("https://localhost", "http://localhost:3000");
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();

            });

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





            //Swagger

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevOrc.Try.API v1"));
            }

        }
    }
}
