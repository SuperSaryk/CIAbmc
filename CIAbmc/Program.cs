using Microsoft.EntityFrameworkCore;
using CIAbmc.Data;
using System.Diagnostics;

namespace CIAbmc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); 
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));
            var app = builder.Build();

            //static void OpenXpdf() 
            //{
            //    Process cmd = new Process();
            //    cmd.StartInfo.FileName = "cmd.exe";
            //    cmd.StartInfo.RedirectStandardInput = true;
            //    cmd.StartInfo.RedirectStandardOutput = true;
            //    cmd.StartInfo.CreateNoWindow = true;
            //    cmd.StartInfo.UseShellExecute = false;
            //    cmd.Start();

            //    cmd.StandardInput.WriteLine(@"cd C:\Program Files\Glyph & Cog\XpdfReader-win64\xpdf.exe");
            //    cmd.StandardInput.Flush();
            //    cmd.StandardInput.Close();
            //    cmd.WaitForExit();
            //    Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            //}

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
