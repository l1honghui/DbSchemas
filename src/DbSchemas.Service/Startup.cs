using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DbSchemas.Service.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "DbSchemas API"
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "DbSchemas.Service.xml");

                //var xmlPathModel = Path.Combine(AppContext.BaseDirectory, "DbSchemas.Model.xml");
                options.IncludeXmlComments(xmlPath);
                //options.IncludeXmlComments(xmlPathModel);
                options.IgnoreObsoleteActions();
            });
            services.AddMvc();

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(option =>
            {
                option.AllowAnyHeader();
                option.AllowAnyMethod();
                option.AllowAnyOrigin();
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
             
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DbSchemas API V1");
            });
            //app.Map("/", appbulder =>
            //{
            //    appbulder.Run(async context =>
            //    {
            //        context.Response.Redirect("/index.html");
            //        await Task.FromResult(0);
            //    });
            //});
        }
    }
}
