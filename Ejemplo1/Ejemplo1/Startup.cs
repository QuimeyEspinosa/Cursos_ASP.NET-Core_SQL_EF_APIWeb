using Ejemplo1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IProductoComercio, MockProductoComercio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment()) //Indicamos que es un entorno de desarrollo
            {
                DeveloperExceptionPageOptions devPage = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 2
                };
                app.UseDeveloperExceptionPage(devPage);
            }
            else if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute(); //Utiliza HomeController como ruta por defecto

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            #region Comentarios

            /*
            //Definir nuestro propio enrutamiento
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Metodo Run");
            });
            */

            /*
            DefaultFilesOptions archivosDefecto = new DefaultFilesOptions();
            archivosDefecto.DefaultFileNames.Clear(); //Limpia los archivos por defecto
            archivosDefecto.DefaultFileNames.Add("index.html"); //le indicamos cual/es son los archivos por defecto

            app.UseDefaultFiles(archivosDefecto); //Nos permite utilizar los archivos por defecto (Por ejemplo index.html)
            app.UseStaticFiles(); //Nos permite utilizar archivos estáticos (Por ejemplo imágenes)
            */


            /*
            app.Use(async (context, next) =>
            {
                logger.LogInformation("Registro1");
                await context.Response.WriteAsync("Camino 1");
                await next();
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("Registro2");

                await context.Response.WriteAsync("Camino 2");
                await next();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            */

            #endregion
        }
    }
}
