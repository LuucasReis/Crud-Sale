using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppVendas.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using AppVendas.Models;
using AppVendas.Models.Services;
namespace AppVendas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<SeedingService>();
            builder.Services.AddScoped<VendedorService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<RegistroVendasService>();

            string ConnectionString= builder.Configuration.GetConnectionString("DepartmentsContext");
            builder.Services.AddDbContext<DepartmentsContext>(options =>
                options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), builder => builder.MigrationsAssembly("AppVendas")));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            var ptBR = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ptBR),
                SupportedCultures = new List<CultureInfo> { ptBR },
                SupportedUICultures = new List<CultureInfo> { ptBR }
            };

            app.UseRequestLocalization(localizationOptions);


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            else
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
                using (var scope= scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<SeedingService>();
                    service.Seed();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}