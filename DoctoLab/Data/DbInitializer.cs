using DoctoLab.Models;
using Microsoft.AspNetCore.Identity;

namespace DoctoLab.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<AppUser> userManager)
    {
        string[] roles = { "Admin", "Doctor", "Patient" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = "admin@doctolab.com";

        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            var newAdmin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Role = "Admin",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin123!");

            if (result.Succeeded)
                await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}