using Domain.JobPost;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public class JobPostSeeder
    {
        public static async Task SeedJobPostsAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            string adminId = "", employerId = "";
            var categoryId = 0;
            var admin = await userManager.FindByEmailAsync("admin@test.com");
            if (admin == null)
            {
                throw new Exception("Admin user not found");
            }
            adminId = admin.Id;

            var employer = await userManager.FindByEmailAsync("employer@test.com");
            if (employer == null)
            {
                throw new Exception("Employer user not found");
            }
            employerId = employer.Id;

            var category = await context.JobCategories.FirstOrDefaultAsync(c => c.Name == "Other");
            if (category == null)
            {
                throw new Exception($"Category property {nameof(category.Name)} not found");
            }
            categoryId = category.Id;


            await CreateJobPostAsync(context, "Test1", "Test1", "Test1", "Test1", "Test1", adminId, categoryId);
            await CreateJobPostAsync(context, "Test2", "Test2", "Test2", "Test2", "Test2", adminId, categoryId);
            await CreateJobPostAsync(context, "Test3", "Test3", "Test3", "Test3", "Test3", employerId, categoryId);
            await CreateJobPostAsync(context, "Test4", "Test4", "Test4", "Test4", "Test4", employerId, categoryId);
        }
        private static async Task CreateJobPostAsync(ApplicationDbContext context, string title, string Company, string description, string salary, string location, string userId, int jobCategoryId)
        {
            if (!await context.JobPosts.AnyAsync(j => j.Title == title))
            {
                var jobPost = new Domain.JobPost.JobPost()
                {
                    Title = title,
                    Company = Company,
                    Description = description,
                    Salary = salary,
                    Location = location,
                    Created = DateTime.Now,
                    IsApproved = true,
                    UserId = userId,
                    JobCategoryId = jobCategoryId
                };

                await context.AddAsync(jobPost);
                await context.SaveChangesAsync();
            }
        }
    }
}
