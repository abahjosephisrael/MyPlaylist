using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;

namespace MyPlaylistApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Field delcaration
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplication();
            #region Swagger
            services.AddSwaggerGen(s =>
            {
                s.IncludeXmlComments(string.Format(@"{0}\MyPlaylistApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyPlaylistApi",
                    Description = "This application enables users to search and create a custom video playlist."
                });
            });
            #endregion

            //Bringing in the dependency injection class from persistence where services are registered.
            services.AddPersistence(Configuration);
        }
        /// <summary>
        /// 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            #region Swagger
            //Enable middleware to serve generated Swagger as JSON endpoint
            app.UseSwagger();

            //Enable middleware to serve Swagger-ui (HTML, JS, CSS),
            //specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "MyPlaylistApi");
            });
            #endregion
        }
    }
}
