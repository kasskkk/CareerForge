using Domain.JobPost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public class JobCategorySeeder
    {
        public static async Task SeedCategoriesAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var categories = new List<string>()
            {
                "IT - Programing",
                "Accounting",
                "Finance",
                "Marketing",
                "Construction",
                "Other"
            };

            foreach (var category in categories)
            {
                if (!context.JobCategories.Any(c => c.Name == category))
                {
                    await CreateJobCategoryAsync(context, category);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateJobCategoryAsync(ApplicationDbContext context, string name)
        {
            var category = new JobCategory()
            {
                Name = name
            };

            await context.AddAsync(category);
            await context.SaveChangesAsync();
        }
    }
}
