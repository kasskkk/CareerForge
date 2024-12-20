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
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleMenager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleMenager.RoleExistsAsync(Role.ADMIN))
            {
                await roleMenager.CreateAsync(new IdentityRole(Role.ADMIN));
            }

            if (!await roleMenager.RoleExistsAsync(Role.JOBSEEKER))
            {
                await roleMenager.CreateAsync(new IdentityRole(Role.JOBSEEKER));
            }

            if (!await roleMenager.RoleExistsAsync(Role.EMPLOYER))
            {
                await roleMenager.CreateAsync(new IdentityRole(Role.EMPLOYER));
            }
        }
    }
}
