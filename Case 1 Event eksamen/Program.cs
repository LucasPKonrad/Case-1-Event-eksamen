using Case_1_Event_eksamen.Pages.Services;
using Microsoft.EntityFrameworkCore;
using Case_1_Event_eksamen.Pages.Data;


namespace Case_1_Event_eksamen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<PasswordHasher>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddSession();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddDbContext<AppDbContexxt>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
