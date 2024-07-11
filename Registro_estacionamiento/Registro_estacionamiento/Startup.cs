namespace Registro_estacionamiento
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuración de servicios
            services.AddControllersWithViews();
            // Otros servicios como Entity Framework, servicios personalizados, etc.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Página de error detallada en entorno de desarrollo
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Manejo de errores en otros entornos
                app.UseHsts(); // Configuración de HSTS (opcional)
            }

            app.UseHttpsRedirection(); // Redirecciona HTTP a HTTPS (opcional)
            app.UseStaticFiles(); // Habilita el uso de archivos estáticos como CSS, JS, etc.

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}
