using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constant;

namespace Infrastructure.Data.Seeder
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var usermenager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRoleAsync(usermenager, "admin@test.com", "Admin!123", Role.ADMIN);
            await CreateUserWithRoleAsync(usermenager, "jobseeker@test.com", "Jobseeker!123", Role.JOBSEEKER);
            await CreateUserWithRoleAsync(usermenager, "employertest.com", "Employer!123", Role.EMPLOYER);
        }

        private static async Task CreateUserWithRoleAsync(UserManager<IdentityUser> usermanager, string email, string password, string role)
        {
            if (await usermanager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser()
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await usermanager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}, Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
