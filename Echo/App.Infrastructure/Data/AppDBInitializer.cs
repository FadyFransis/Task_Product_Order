using App.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace App.Infrastructure.Data
{
    public static class AppDBInitializer
    {
        public static void SeedSuperAdminUser(UserManager<AppUser> userManager, AppDBContext context)
        {
            if (context.Users.FirstOrDefault(u => u.Email == "admin@app.com") == null)
            {
                var defaultUser = new AppUser
                {
                    UserName = "admin@App.com",
                    Email = "admin@App.com",
                    FirstName = "Admin",
                    LastName = "App",
                    PhoneNumber = "01234567890",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(defaultUser, "Qwe_123456").Wait();
                userManager.AddToRoleAsync(defaultUser, "SuperAdmin").Wait();
                var res = userManager.AddClaimsAsync(defaultUser, new[]
                              {
                                new Claim("UserFullName", "Admin App")
                            }).Result;
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "SuperAdmin"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Client").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Client"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

        }

        public static void SeedAppSetting(AppDBContext context)
        {
            List<AppSetting> AppSettings = new List<AppSetting>();
            if (context.AppSetting.FirstOrDefault() == null)
            {

                for (int i = 1; i <= 14; i++)
                {
                    AppSetting appSetting = new AppSetting
                    {
                        Key = (Core.Entities.Base.AppSettingKey)i,
                        CreationDate = DateTime.Now,
                        LastUpdatedDate = DateTime.Now
                    };
                    AppSettings.Add(appSetting);

                }
                context.AppSetting.AddRange(AppSettings);
                context.SaveChanges();
            }
        }

        
    }
}
