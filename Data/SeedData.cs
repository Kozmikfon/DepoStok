using DepoStok.Models.Identity;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        // 1️⃣ Rolleri oluştur
        string[] roles = { "admin", "kullanici" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        // 2️⃣ Admin kullanıcı oluştur
        await CreateUserIfNotExists(
            userManager,
            email: "admin@depo.com",
            adSoyad: "Yönetici",
            password: "Admin123!",
            role: "admin"
        );

        // 3️⃣ Örnek normal kullanıcı oluştur
        await CreateUserIfNotExists(
            userManager,
            email: "kullanici1@depo.com",
            adSoyad: "Ahmet Kullanıcı",
            password: "Kullanici123!",
            role: "kullanici"
        );
    }

    private static async Task CreateUserIfNotExists(
     UserManager<AppUser> userManager,
     string email,
     string adSoyad,
     string password,
     string role)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new AppUser
            {
                UserName = email,
                Email = email,
                AdSoyad = adSoyad,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception($"{email} kullanıcısı oluşturulamadı: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // ❗ Kullanıcı varsa bile rolü eksik olabilir, kontrol edelim:
        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }

}
