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
    public class depoTransfersController : Controller
    {
        private readonly StokDbContext _context;

        public depoTransfersController(StokDbContext context)
        {
            _context = context;
        }

        // GET: depoTransfers
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.depoTransferleri.Include(d => d.hedefDepo).Include(d => d.kaynakDepo);
            return View(await stokDbContext.ToListAsync());
        }

        // GET: depoTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depoTransfer = await _context.depoTransferleri
                .Include(d => d.hedefDepo)
                .Include(d => d.kaynakDepo)
                .FirstOrDefaultAsync(m => m.transferId == id);
            if (depoTransfer == null)
            {
                return NotFound();
            }

            return View(depoTransfer);
        }

        // GET: depoTransfers/Create
        public IActionResult Create()
        {
            ViewData["hedefDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd");
            ViewData["kaynakDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd");
            return View();
        }

        // POST: depoTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferId,transferNo,kaynakDepoId,hedefDepoId,transferTarihi,aciklama,seriNo")] depoTransfer depoTransfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depoTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hedefDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", depoTransfer.hedefDepoId);
            ViewData["kaynakDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", depoTransfer.kaynakDepoId);
            return View(depoTransfer);
        }

        // GET: depoTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depoTransfer = await _context.depoTransferleri.FindAsync(id);
            if (depoTransfer == null)
            {
                return NotFound();
            }
            ViewData["hedefDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", depoTransfer.hedefDepoId);
            ViewData["kaynakDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", depoTransfer.kaynakDepoId);
            return View(depoTransfer);
        }

        // POST: depoTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("transferId,transferNo,kaynakDepoId,hedefDepoId,transferTarihi,aciklama,seriNo")] depoTransfer depoTransfer)
        {
            if (id != depoTransfer.transferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depoTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!depoTransferExists(depoTransfer.transferId))
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
            ViewData["hedefDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", depoTransfer.hedefDepoId);
            ViewData["kaynakDepoId"] = new SelectList(_context.depolar, "depoId", "depoAd", depoTransfer.kaynakDepoId);
            return View(depoTransfer);
        }

        // GET: depoTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depoTransfer = await _context.depoTransferleri
                .Include(d => d.hedefDepo)
                .Include(d => d.kaynakDepo)
                .FirstOrDefaultAsync(m => m.transferId == id);
            if (depoTransfer == null)
            {
                return NotFound();
            }

            return View(depoTransfer);
        }

        // POST: depoTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depoTransfer = await _context.depoTransferleri.FindAsync(id);
            if (depoTransfer != null)
            {
                _context.depoTransferleri.Remove(depoTransfer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool depoTransferExists(int id)
        {
            return _context.depoTransferleri.Any(e => e.transferId == id);
        }
    }
}
