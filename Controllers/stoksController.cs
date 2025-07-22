using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStok.Data;
using DepoStok.Models;

namespace DepoStok.Controllers
{
    public class stoksController : Controller
    {
        private readonly StokDbContext _context;

        public stoksController(StokDbContext context)
        {
            _context = context;
        }

        // GET: stoks
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.stoklar.Include(s => s.Depo).Include(s => s.Malzeme);
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
            ViewData["DepoId"] = new SelectList(_context.depolar, "depoId", "depoAd");
            ViewData["MalzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi");
            return View();
        }

        // POST: stoks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HareketId,MalzemeId,DepoId,HareketTarihi,Miktar,HareketTipi,ReferansId,Aciklama,SeriNo")] stok stok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", stok.DepoId);
            ViewData["MalzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi", stok.MalzemeId);
            return View(stok);
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
        public async Task<IActionResult> Edit(int id, [Bind("HareketId,MalzemeId,DepoId,HareketTarihi,Miktar,HareketTipi,ReferansId,Aciklama,SeriNo")] stok stok)
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
