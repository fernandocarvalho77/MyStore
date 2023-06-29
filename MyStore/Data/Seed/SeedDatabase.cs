using Microsoft.AspNetCore.Identity;

namespace MyStore.Data.Seed;

public class SeedDatabase
{
    public static void Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        SeedRoles(roleManager).Wait();
        SeedUsers(userManager).Wait();
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        var roleCheck = await roleManager.RoleExistsAsync(MyStoreConstants.Roles.ADMINISTRATOR);

        if (!roleCheck)
        {
            var adminRole = new IdentityRole
            {
                Name = MyStoreConstants.Roles.ADMINISTRATOR
            };

            await roleManager.CreateAsync(adminRole);
        }

        roleCheck = await roleManager.RoleExistsAsync(MyStoreConstants.Roles.WORKER);

        if (!roleCheck)
        {
            var administrativeRole = new IdentityRole
            {
                Name = MyStoreConstants.Roles.WORKER
            };

            await roleManager.CreateAsync(administrativeRole);
        }
    }

    private static async Task SeedUsers(UserManager<IdentityUser> userManager)
    {
        var dbAdmin = await userManager.FindByNameAsync(MyStoreConstants.Users.Administrator.USERNAME);

        if (dbAdmin == null)
        {
            var result = await userManager.CreateAsync(
                new IdentityUser
                {
                    UserName = MyStoreConstants.Users.Administrator.USERNAME,
                    Email = MyStoreConstants.Users.Administrator.USERNAME
                },
                MyStoreConstants.Users.Administrator.PASSWORD
            );

            if (result.Succeeded)
            {
                dbAdmin = await userManager.FindByNameAsync(MyStoreConstants.Users.Administrator.USERNAME);
                await userManager.AddToRoleAsync(dbAdmin!, MyStoreConstants.Roles.ADMINISTRATOR);
            }
        }

        var dbWorker = await userManager.FindByNameAsync(MyStoreConstants.Users.Worker.USERNAME);

        if (dbWorker == null)
        {
            var result = await userManager.CreateAsync(
                new IdentityUser
                {
                    UserName = MyStoreConstants.Users.Worker.USERNAME,
                    Email = MyStoreConstants.Users.Worker.USERNAME
                },
                MyStoreConstants.Users.Worker.PASSWORD
            );

            if (result.Succeeded)
            {
                dbWorker = await userManager.FindByNameAsync(MyStoreConstants.Users.Worker.USERNAME);
                await userManager.AddToRoleAsync(dbWorker!, MyStoreConstants.Roles.WORKER);
            }
        }
    }
}