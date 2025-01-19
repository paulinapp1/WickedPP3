using System.Reflection.Emit;
using WickedGame.Services;
using WickedLogic;

namespace WickedGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<GameService>();

            builder.Services.AddSingleton<GameInstance>(sp => new GameInstance(Level.GetLevel(Levels.Medium)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapFallbackToFile("index.html"); // Serve React app for unknown routes

            app.Run();
        }
    }
}
