using DataAccess.EF;
using DataAccess.Entitty;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPlusAngular.Helper
{
    public class SeederDatabase
    {
        public static void SeedData(IServiceProvider services,
        IWebHostEnvironment env,
        IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                SeedUsers(manager, managerRole);
            }
        }
        private static void SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleName = "Admin";
            if (roleManager.FindByNameAsync(roleName).Result == null)
            {
                var resultAdminRole = roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                }).Result;

                var resultUserRole = roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User"
                }).Result;
            }

            string email = "admin@gmail.com";
            var admin = new User
            {
                Email = email,
                UserName = email,
                Address = "Rivne",
                Age = 17,
                FullName = "Mykola Zaiets"
            };
            //var andrii = new User
            //{
            //    Email = "zaietsmikola.21@gmail.com",
            //    UserName = "zaietsmikola.21@gmail.com"
            //};

            var resultAdmin = userManager.CreateAsync(admin, "Qwerty1-").Result;
            resultAdmin = userManager.AddToRoleAsync(admin, "Admin").Result;


            //var resultAndrii = userManager.CreateAsync(andrii, "Qwerty1-").Result;
            //resultAndrii = userManager.AddToRoleAsync(andrii, "User").Result;
        }
    }
}
