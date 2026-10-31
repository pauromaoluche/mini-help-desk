using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Infrastructure.Data
{
    public static class UserSeeder
    {
        public static async Task SeedAdminUserAsync(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            const string adminEmail = "admin@hotmail.com";

            var adminExists = await context.Users.AnyAsync(u => u.Email == adminEmail);
            if (adminExists)
            {
                return;
            }

            var adminUser = new User(
                "Admin",
                adminEmail,
                passwordHasher.HashPassword(null, "12345678"),
                UserRoleEnum.Admin
            );

            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }
}
