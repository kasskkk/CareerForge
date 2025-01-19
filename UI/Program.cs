using Application.JobApplication;
using Application.JobPost;
using Application.ReportedPostService;
using Application.User;
using Domain.Generic;
using Domain.JobPost;
using Infrastructure.Data;
using Infrastructure.Data.Seeder;
using Infrastructure.Generic;
using Infrastructure.JobPost;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
                .AddScoped<IJobPostRepository, JobPostRepository>()
                .AddScoped<IJobPostService, JobPostService>()
                .AddScoped<IJobCategoryRepository, JobCategoryRepository>()
                .AddScoped<IReportedJobPostService, ReportedJobPostService>()
                .AddScoped<IUserInfoService, UserInfoService>()
                .AddScoped<IJobApplicationService, JobApplicationService>();
                
            

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                RoleSeeder.SeedRolesAsync(services).Wait();

                UserSeeder.SeedUsersAsync(services).Wait();

                JobCategorySeeder.SeedCategoriesAsync(services).Wait();

                JobPostSeeder.SeedJobPostsAsync(services).Wait();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=JobPost}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
