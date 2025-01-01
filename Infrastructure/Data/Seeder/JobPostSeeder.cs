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
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            await CreateJobPostAsync(context,"Test1","Test1","Test1", "Test1","Test1", "2fbb398a-e3a9-4d2d-9f95-6c38b7def50f");
            await CreateJobPostAsync(context,"Test2","Test2","Test2", "Test2","Test2", "2fbb398a-e3a9-4d2d-9f95-6c38b7def50f");
            await CreateJobPostAsync(context,"Test3","Test3","Test3", "Test3","Test3", "4ec73766-9786-4afd-8cda-530860a979c6");
            await CreateJobPostAsync(context,"Test4","Test4","Test4", "Test4","Test4", "4ec73766-9786-4afd-8cda-530860a979c6");
        }
        private static async Task CreateJobPostAsync(ApplicationDbContext context, string title, string Company, string description,string salary, string location, string userId)
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
                    UserId = userId
                };

                await context.AddAsync(jobPost);
                await context.SaveChangesAsync();
            }
        }
    }
}
