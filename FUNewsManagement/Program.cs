using FUNewsManagement.Repository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace FUNewsManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);

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

            app.UseSession();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapGet("/", () => Results.Redirect("/Home/Index"));

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddDbContext<NewsDBContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            service.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(1);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });

            service.AddTransient<INewsRepository, NewsRepository>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<ITagRepository, TagRepository>();
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<ICommentRepository, CommentRepository>();

            service.AddRazorPages();
        }
    }
}
