using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentsPlayground.Data;
using PaymentsPlayground.Helpers.Const;
using PaymentsPlayground.Models;

namespace PaymentsPlayground.Helpers
{
    public static class Seed
    {
        public static async Task<IServiceProvider> SeedDatabase(this IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await context.Database.MigrateAsync();
                await Seed.SeedRoles(roleManager);
                await Seed.SeedAdmin(userManager, roleManager, context);
            }
            catch(Exception ex){
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }

            return provider;
        }

        private static async Task SeedAdmin(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            var admin = dbContext.Users.FirstOrDefault(x => x.Email == "app.admin@gmail.com");

            if (admin != null) return;

            var adminCreated = new User
            {
                Email = "app.admin@gmail.com",
                UserName = "app.admin@gmail.com"
            };

            await userManager.CreateAsync(adminCreated, "ssPassword");

            await userManager.AddToRoleAsync(adminCreated, RoleConstants.Admin);

            var wallet = new Wallet
            {
                Account = 1000,
                UserId = adminCreated.Id
            };

            dbContext.Wallets.Add(wallet);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(RoleConstants.Admin))
                await roleManager.CreateAsync(new IdentityRole { Name = RoleConstants.Admin });
            if (!await roleManager.RoleExistsAsync(RoleConstants.User))
                await roleManager.CreateAsync(new IdentityRole { Name = RoleConstants.User });
        }
    }
}
