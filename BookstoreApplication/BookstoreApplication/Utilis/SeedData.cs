using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Utils
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Kreiranje prvog editora
            var editor1 = new ApplicationUser
            {
                UserName = "john",
                Email = "john.doe@example.com",
                Name = "John",
                Surname = "Doe",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1985, 5, 15), DateTimeKind.Utc), // ✅ UTC
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(editor1.UserName) == null)
            {
                await userManager.CreateAsync(editor1, "John123!");
                await userManager.AddToRoleAsync(editor1, "Editor");
            }

            // Kreiranje drugog editora
            var editor2 = new ApplicationUser
            {
                UserName = "jane",
                Email = "jane.doe@example.com",
                Name = "Jane",
                Surname = "Doe",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1990, 8, 22), DateTimeKind.Utc), // ✅ UTC
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(editor2.UserName) == null)
            {
                await userManager.CreateAsync(editor2, "Jane123!");
                await userManager.AddToRoleAsync(editor2, "Editor");
            }

            // Kreiranje treceg editora
            var editor3 = new ApplicationUser
            {
                UserName = "nick",
                Email = "nick.smith@example.com",
                Name = "Nick",
                Surname = "Smith",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1987, 12, 3), DateTimeKind.Utc), // ✅ UTC
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(editor3.UserName) == null)
            {
                await userManager.CreateAsync(editor3, "Nick123!");
                await userManager.AddToRoleAsync(editor3, "Editor");
            }
        }
    }
}