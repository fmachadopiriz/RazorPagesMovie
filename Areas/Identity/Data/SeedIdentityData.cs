using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RazorPagesMovie.Areas.Identity.Data
{
    public static class SeedIdentityData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync(IdentityData.AdminUserName).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Name = IdentityData.AdminName;
                user.UserName = IdentityData.AdminUserName;
                user.Email = IdentityData.AdminMail;
                user.DOB = IdentityData.AdminDOB;

                IdentityResult result = userManager.CreateAsync(user, IdentityData.AdminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, IdentityData.AdminRoleName).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(IdentityData.AdminRoleName).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = IdentityData.AdminRoleName;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}