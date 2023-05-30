using Count.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.DataAccess.Seeder
{
    public class Seeder : ISeeder
    {
        public async Task SeedAsync(CountDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            await SeedRolesAsync(roleManager, "Admin");
            await SeedRolesAsync(roleManager, "Agent");

            await SeedUserWithRoleAdminAsync(userManager);
            await SeedUserWithRoleAgentAsync(userManager);
        }

        private async Task SeedUserWithRoleAdminAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Admin");
            if (user == null)
            {
                var result = await userManager.CreateAsync(new User
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin"

                }, "Admin_12345");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                else
                {
                    user = await userManager.FindByNameAsync("Admin");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }

        private async Task SeedUserWithRoleAgentAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Agent");
            if (user == null)
            {
                var result = await userManager.CreateAsync(new User
                {
                    UserName = "Agent",
                    Email = "agent@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Agent",
                    LastName = "Agent"

                }, "Agent_12345");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                else
                {
                    user = await userManager.FindByNameAsync("Agent");
                    await userManager.AddToRoleAsync(user, "Agent");
                }
            }

        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
