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

        // GET: irsaliyeDetays
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.irsaliyeDetaylari.Include(i => i.irsaliye).Include(i => i.malzeme);
            return View(await stokDbContext.ToListAsync());
        }

        // GET: irsaliyeDetays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irsaliyeDetay = await _context.irsaliyeDetaylari
                .Include(i => i.irsaliye)
                .Include(i => i.malzeme)
                .FirstOrDefaultAsync(m => m.detayId == id);
            if (irsaliyeDetay == null)
            {
                return NotFound();
            }

            return View(irsaliyeDetay);
        }

        // GET: irsaliyeDetays/Create
        public IActionResult Create()
        {
            ViewData["irsaliyeId"] = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo");
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim");
            return View();
        }

        // POST: irsaliyeDetays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("detayId,irsaliyeId,malzemeId,miktar,birimFiyat,araToplam,seriNo")] irsaliyeDetay irsaliyeDetay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(irsaliyeDetay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["irsaliyeId"] = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo", irsaliyeDetay.irsaliyeId);
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim", irsaliyeDetay.malzemeId);
            return View(irsaliyeDetay);
        }

        // GET: irsaliyeDetays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irsaliyeDetay = await _context.irsaliyeDetaylari.FindAsync(id);
            if (irsaliyeDetay == null)
            {
                return NotFound();
            }
            ViewData["irsaliyeId"] = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo", irsaliyeDetay.irsaliyeId);
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim", irsaliyeDetay.malzemeId);
            return View(irsaliyeDetay);
        }

        // POST: irsaliyeDetays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("detayId,irsaliyeId,malzemeId,miktar,birimFiyat,araToplam,seriNo")] irsaliyeDetay irsaliyeDetay)
        {
            if (id != irsaliyeDetay.detayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(irsaliyeDetay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!irsaliyeDetayExists(irsaliyeDetay.detayId))
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
            ViewData["irsaliyeId"] = new SelectList(_context.irsaliyeler, "irsaliyeId", "irsaliyeNo", irsaliyeDetay.irsaliyeId);
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim", irsaliyeDetay.malzemeId);
            return View(irsaliyeDetay);
        }

        // GET: irsaliyeDetays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irsaliyeDetay = await _context.irsaliyeDetaylari
                .Include(i => i.irsaliye)
                .Include(i => i.malzeme)
                .FirstOrDefaultAsync(m => m.detayId == id);
            if (irsaliyeDetay == null)
            {
                return NotFound();
            }

            return View(irsaliyeDetay);
        }

        // POST: irsaliyeDetays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var irsaliyeDetay = await _context.irsaliyeDetaylari.FindAsync(id);
            if (irsaliyeDetay != null)
            {
                _context.irsaliyeDetaylari.Remove(irsaliyeDetay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool irsaliyeDetayExists(int id)
        {
            return _context.irsaliyeDetaylari.Any(e => e.detayId == id);
        }
    }
}
