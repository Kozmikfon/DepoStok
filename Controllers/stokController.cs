using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DepoStok.Data;

namespace DepoStok.Controllers
{
    public class stokController : Controller
    {
        private readonly StokDbContext _context;

        public stokController(StokDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hareketler = await _context.stoklar
                .Include(s => s.Malzeme)
                .Include(s => s.Depo)
                .OrderByDescending(s => s.HareketTarihi)
                .ToListAsync();

            return View(hareketler);
        }
    }
}
