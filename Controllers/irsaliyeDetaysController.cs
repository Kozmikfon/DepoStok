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
    public class irsaliyeDetaysController : Controller
    {
        private readonly StokDbContext _context;

        public irsaliyeDetaysController(StokDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.irsaliyeDetaylari.Include(i => i.irsaliye).Include(i => i.malzeme);
            return View(await stokDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var irsaliyeDetay = await _context.irsaliyeDetaylari
                .Include(i => i.irsaliye)
                .Include(i => i.malzeme)
                .FirstOrDefaultAsync(m => m.detayId == id);

            if (irsaliyeDetay == null) return NotFound();

            return View(irsaliyeDetay);
        }

        public IActionResult Create(int? irsaliyeId)
        {
            ViewBag.irsaliyeId = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo",irsaliyeId);
            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("detayId,irsaliyeId,malzemeId,miktar,birimFiyat,araToplam,seriNo")] irsaliyeDetay irsaliyeDetay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(irsaliyeDetay);
                await _context.SaveChangesAsync();

                var irsaliye = await _context.irsaliyeler.FindAsync(irsaliyeDetay.irsaliyeId);
                if (irsaliye != null)
                {
                    var stok = new stok
                    {
                        MalzemeId = irsaliyeDetay.malzemeId,
                        DepoId = irsaliye.depoId,
                        HareketTarihi = irsaliye.irsaliyeTarihi,
                        Miktar = irsaliyeDetay.miktar,
                        HareketTipi = irsaliye.irsaliyeTipi,
                        ReferansId = irsaliye.irsaliyeId,
                        Aciklama = irsaliye.aciklama,
                        carId = irsaliye.carId,
                        SeriNo = irsaliyeDetay.seriNo
                    };

                    _context.stoklar.Add(stok);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.irsaliyeId = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo", irsaliyeDetay.irsaliyeId);
            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAd", irsaliyeDetay.malzemeId);
            return View(irsaliyeDetay);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var irsaliyeDetay = await _context.irsaliyeDetaylari.FindAsync(id);
            if (irsaliyeDetay == null) return NotFound();

            ViewBag.irsaliyeId = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo", irsaliyeDetay.irsaliyeId);
            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAd", irsaliyeDetay.malzemeId);
            return View(irsaliyeDetay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("detayId,irsaliyeId,malzemeId,miktar,birimFiyat,araToplam,seriNo")] irsaliyeDetay irsaliyeDetay)
        {
            if (id != irsaliyeDetay.detayId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(irsaliyeDetay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!irsaliyeDetayExists(irsaliyeDetay.detayId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.irsaliyeId = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo", irsaliyeDetay.irsaliyeId);
            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAd", irsaliyeDetay.malzemeId);
            return View(irsaliyeDetay);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var irsaliyeDetay = await _context.irsaliyeDetaylari
                .Include(i => i.irsaliye)
                .Include(i => i.malzeme)
                .FirstOrDefaultAsync(m => m.detayId == id);

            if (irsaliyeDetay == null) return NotFound();

            return View(irsaliyeDetay);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var irsaliyeDetay = await _context.irsaliyeDetaylari.FindAsync(id);
            if (irsaliyeDetay != null)
            {
                _context.irsaliyeDetaylari.Remove(irsaliyeDetay);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool irsaliyeDetayExists(int id)
        {
            return _context.irsaliyeDetaylari.Any(e => e.detayId == id);
        }
    }
}
