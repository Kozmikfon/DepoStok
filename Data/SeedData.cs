using DepoStok.Models.Identity;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        // 1. Gerekli rollerin varlığını kontrol et, yoksa oluştur
        string[] roles = { "admin", "kullanici" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        // 2. Varsayılan admin kullanıcı oluşturuluyor
        var adminEmail = "admin@depo.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var user = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                AdSoyad = "Yönetici",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Admin123!"); // Şifre karma yapılır
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
            else
            {
                // Geliştiriciye hata loglamak için mesaj
                throw new Exception($"Admin kullanıcı oluşturulamadı: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
