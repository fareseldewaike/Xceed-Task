using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoool.infrastructure.Seed
{
    public static class ApplicationDbSeeder
    {
        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

 
            string[] roles = { "Admin", "Customer"};

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

 
            string adminEmail = "fares@productcatalog.com";
            string adminPassword = "Admin@123";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = "admin",
                    Email = adminEmail,

                };

                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
            
            string customerEmail = "emad@productcatalog.com";
            string customerPassword = "Admin@123";
            var customerUser = await userManager.FindByEmailAsync(customerEmail);

            if (customerUser == null)
            {
                var newCustomer = new IdentityUser
                {
                    UserName = "customer",
                    Email = customerEmail
                };

                var result = await userManager.CreateAsync(newCustomer, customerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newCustomer, "Customer");
                }
            }

        }
    }
}
