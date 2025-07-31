using DepoStok.Models.Identity;
using DepoStok.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepoStok.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Admin
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userList = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new AdminUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = string.Join(", ", roles)
                });
            }

            return View(userList);
        }

        [HttpGet]
        public IActionResult KullaniciEkle()
        {
            ViewBag.Roller = new List<string> { "admin", "kullanici" };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciEkle(KullaniciEkleViewModel model)
        {
            ViewBag.Roller = new List<string> { "admin", "kullanici" };

            if (!ModelState.IsValid)
                return View(model);

            var mevcut = await _userManager.FindByEmailAsync(model.Email);
            if (mevcut != null)
            {
                ModelState.AddModelError("", "Bu email adresi zaten kayıtlı.");
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                AdSoyad = model.AdSoyad,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Sifre);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                return View(model);
            }

            if (!await _roleManager.RoleExistsAsync(model.Rol))
                await _roleManager.CreateAsync(new IdentityRole<int>(model.Rol));

            await _userManager.AddToRoleAsync(user, model.Rol);

            TempData["basari"] = "Kullanıcı başarıyla oluşturuldu.";
            return RedirectToAction("KullaniciEkle");
        }
    }

    public class AdminUserViewModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Roles { get; set; }
    }
}
