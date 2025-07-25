using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStok.Data;
using DepoStok.Models;
using DepoStok.Services;
using System.Security.Claims;

namespace DepoStok.Controllers
{
    public class stoksController : Controller
    {
        private readonly StokDbContext _context;

        private readonly StokService _stokService;
        private readonly LogService _logService;
        public stoksController(StokDbContext context,StokService stokService, LogService logService)
        {
            _context = context;
            _stokService = stokService;
            _logService = logService;

        }

        // GET: stoks
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.stoklar
    .Include(s => s.Depo)
    .Include(s => s.Malzeme)
    .Include(s => s.cari);

            return View(await stokDbContext.ToListAsync());
        }

        // GET: stoks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stok = await _context.stoklar
                .Include(s => s.Depo)
                .Include(s => s.Malzeme)
                .FirstOrDefaultAsync(m => m.HareketId == id);
            if (stok == null)
            {
                return NotFound();
            }

            return View(stok);
        }

        // GET: stoks/Create
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        // POST: stoks/Create
                        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HareketId,MalzemeId,DepoId,HareketTarihi,Miktar,HareketTipi,ReferansId,Aciklama,SeriNo,carId")] stok stok)
        {
            if (stok.carId == null)
            {
                ModelState.AddModelError("carId", "Cari seçimi zorunludur.");
            }

            if (!ModelState.IsValid)
            {
                LoadDropdowns(stok);
                return View(stok);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _stokService.AddStokAsync(stok, userId, stok.carId.Value);

            var detay = $"Stok hareketi eklendi: MalzemeId={stok.MalzemeId}, Miktar={stok.Miktar}, Tip={stok.HareketTipi}";
            await _logService.LogIslemAsync(userId, "stok", "Ekleme", detay);

            return RedirectToAction(nameof(Index));

            
        }

        private void LoadDropdowns(stok s = null)
        {
            ViewData["DepoId"] = new SelectList(_context.depolar.ToList(), "depoId", "depoAd", s?.DepoId);
            ViewData["MalzemeId"] = new SelectList(_context.malzemeler.ToList(), "malzemeId", "malzemeAdi", s?.MalzemeId);

            ViewData["carId"] = new SelectList(
                new List<SelectListItem>
                {
            new SelectListItem { Value = "", Text = "-- Cari Seçiniz --" }
                }.Concat(
                    _context.cariler.Select(c => new SelectListItem
                    {
                        Value = c.carId.ToString(),
                        Text = c.unvan
                    })
                ),
                "Value", "Text", s?.carId?.ToString()
            );
        }



        // GET: stoks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stok = await _context.stoklar.FindAsync(id);
            if (stok == null)
            {
                return NotFound();
            }
            ViewData["DepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", stok.DepoId);
            ViewData["MalzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi", stok.MalzemeId);
            return View(stok);
        }

        // POST: stoks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HareketId,MalzemeId,DepoId,HareketTarihi,Miktar,HareketTipi,ReferansId,Aciklama,SeriNo,carId")] stok stok)
        {
            if (id != stok.HareketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!stokExists(stok.HareketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", stok.DepoId);
            ViewData["MalzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi", stok.MalzemeId);
            ViewData["carId"] = new SelectList(_context.cariler.ToList(), "carId", "unvan", stok.carId);

            return View(stok);
        }

        // GET: stoks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stok = await _context.stoklar
                .Include(s => s.Depo)
                .Include(s => s.Malzeme)
                .FirstOrDefaultAsync(m => m.HareketId == id);
            if (stok == null)
            {
                return NotFound();
            }

            return View(stok);
        }

        // POST: stoks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stok = await _context.stoklar.FindAsync(id);
            if (stok != null)
            {
                _context.stoklar.Remove(stok);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool stokExists(int id)
        {
            return _context.stoklar.Any(e => e.HareketId == id);
        }
    }
}
