using DepoStok.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Threading.Tasks;

namespace DepoStok.Controllers
{
    public class stokDurumuController : Controller
        
    {
        private readonly StokDbContext _context;
        public stokDurumuController(StokDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var liste = await _context.Vw_StokDurumu.ToListAsync();
            return View(liste);
        }
    }
}
