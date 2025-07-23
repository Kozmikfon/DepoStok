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
    public class depoTransferDetaysController : Controller
    {
        private readonly StokDbContext _context;

        public depoTransferDetaysController(StokDbContext context)
        {
            _context = context;
        }

        // GET: depoTransferDetays
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.depoTransferDetaylari.Include(d => d.depoTransfer).Include(d => d.malzeme);
            return View(await stokDbContext.ToListAsync());
        }

        // GET: depoTransferDetays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depoTransferDetay = await _context.depoTransferDetaylari
                .Include(d => d.depoTransfer)
                .Include(d => d.malzeme)
                .FirstOrDefaultAsync(m => m.detayId == id);
            if (depoTransferDetay == null)
            {
                return NotFound();
            }

            return View(depoTransferDetay);
        }

        // GET: depoTransferDetays/Create
        public IActionResult Create()
        {
            ViewData["transferId"] = new SelectList(_context.depoTransferleri, "transferId", "transferNo");
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim");
            return View();
        }

        // POST: depoTransferDetays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("detayId,transferId,malzemeId,miktar")] depoTransferDetay depoTransferDetay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depoTransferDetay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["transferId"] = new SelectList(_context.depoTransferleri, "transferId", "transferNo", depoTransferDetay.transferId);
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim", depoTransferDetay.malzemeId);
            return View(depoTransferDetay);
        }

        // GET: depoTransferDetays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depoTransferDetay = await _context.depoTransferDetaylari.FindAsync(id);
            if (depoTransferDetay == null)
            {
                return NotFound();
            }
            ViewData["transferId"] = new SelectList(_context.depoTransferleri, "transferId", "transferNo", depoTransferDetay.transferId);
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim", depoTransferDetay.malzemeId);
            return View(depoTransferDetay);
        }

        // POST: depoTransferDetays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("detayId,transferId,malzemeId,miktar")] depoTransferDetay depoTransferDetay)
        {
            if (id != depoTransferDetay.detayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depoTransferDetay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!depoTransferDetayExists(depoTransferDetay.detayId))
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
            ViewData["transferId"] = new SelectList(_context.depoTransferleri, "transferId", "transferNo", depoTransferDetay.transferId);
            ViewData["malzemeId"] = new SelectList(_context.malzemeler, "malzemeId", "birim", depoTransferDetay.malzemeId);
            return View(depoTransferDetay);
        }

        // GET: depoTransferDetays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depoTransferDetay = await _context.depoTransferDetaylari
                .Include(d => d.depoTransfer)
                .Include(d => d.malzeme)
                .FirstOrDefaultAsync(m => m.detayId == id);
            if (depoTransferDetay == null)
            {
                return NotFound();
            }

            return View(depoTransferDetay);
        }

        // POST: depoTransferDetays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depoTransferDetay = await _context.depoTransferDetaylari.FindAsync(id);
            if (depoTransferDetay != null)
            {
                _context.depoTransferDetaylari.Remove(depoTransferDetay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool depoTransferDetayExists(int id)
        {
            return _context.depoTransferDetaylari.Any(e => e.detayId == id);
        }
    }
}
