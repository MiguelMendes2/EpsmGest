using EpsmGest.Data;
using EpsmGest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EpsmGest.Helpers
{
    public class StartupIdentity
    {
        private static readonly string[] Roles = new string[] { "Administrator", "Funcionário DGAR", "Professor" };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<EpsmGestDbContext>();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var user = new IdentityUser { Email = "Admin@epsm.pt", UserName = "Admin@epsm.pt" };
                var result = await userManager.CreateAsync(user, "epsm2021");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
    }
}
