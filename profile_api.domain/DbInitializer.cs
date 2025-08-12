using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using profile_api.domain.DefaultInitializeData;
using profile_api.domain.Entities.User;

namespace profile_api.domain
{
    public static class DbInitializer
    {
        public static async Task SeedDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            try
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                await SeedRolesAndAdminAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ". " + ex.Source);
                throw;
            }
        }
        public static async Task SeedRolesAndAdminAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            try
            {
                var defaultRoles = ApplicationRoles.GetDefaultRoles();
                if (!await roleManager.Roles.AnyAsync())
                {
                    foreach (var defaultRole in defaultRoles)
                    {
                        await roleManager.CreateAsync(defaultRole);
                    }
                }
                if (!await roleManager.RoleExistsAsync(ApplicationRoles.Admin))
                {
                    await roleManager.CreateAsync(new AppRole(ApplicationRoles.Admin));
                }
                AppUser defaultUser = ApplicationAccount.GetAppUser();
                var userByName = await userManager.FindByNameAsync(defaultUser.UserName);
                var userByEmail = await userManager.FindByEmailAsync(defaultUser.Email);
                if (userByName is null && userByEmail is null)
                {
                    var result = await userManager.CreateAsync(defaultUser, "123456?aD");
                    if (result.Succeeded)
                    {
                        foreach (var role in defaultRoles)
                        {
                            await userManager.AddToRoleAsync(defaultUser, role.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
