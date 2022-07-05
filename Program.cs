using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace AppVendas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string ConnectionString= builder.Configuration.GetConnectionString("DepartmentsContext");
            builder.Services.AddDbContext<DepartmentsContext>(options =>
                options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), builder => builder.MigrationsAssembly("AppVendas")));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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