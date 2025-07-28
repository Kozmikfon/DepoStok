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
    public class irsaliyesController : Controller
    {
        private readonly StokDbContext _context;

        public irsaliyesController(StokDbContext context)
        {
            _context = context;
        }

        // GET: irsaliyes
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.irsaliyeler.Include(i => i.cari).Include(i => i.depo).Include(i => i.depoTransfer);
            return View(await stokDbContext.ToListAsync());
        }

        // GET: irsaliyes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irsaliye = await _context.irsaliyeler
                .Include(i => i.cari)
                .Include(i => i.depo)
                .Include(i => i.depoTransfer)
                .FirstOrDefaultAsync(m => m.irsaliyeId == id);
            if (irsaliye == null)
            {
                return NotFound();
            }

            return View(irsaliye);
        }

        // GET: irsaliyes/Create
        public IActionResult Create()
        {

            ViewBag.CariList = new SelectList(_context.cariler, "carId", "unvan");
            ViewBag.DepoList = new SelectList(_context.depolar, "depoId", "depoAd");
            ViewBag.TransferList = new SelectList(_context.depoTransferleri, "transferId", "transferId");

            return View();
        }

        // POST: irsaliyes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("irsaliyeId,irsaliyeNo,carId,irsaliyeTarihi,toplamTutar,irsaliyeTipi,aciklama,transferId,durum,depoId")] irsaliye irsaliye)
        {
            if (ModelState.IsValid)
            {
                _context.Add(irsaliye);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CariList = new SelectList(_context.cariler, "carId", "unvan", irsaliye.carId);
            ViewBag.DepoList = new SelectList(_context.depolar, "depoId", "depoAd", irsaliye.depoId);
            ViewBag.TransferList = new SelectList(_context.depoTransferleri, "transferId", "transferId", irsaliye.transferId);


            return View(irsaliye);
        }

        // GET: irsaliyes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irsaliye = await _context.irsaliyeler.FindAsync(id);
            if (irsaliye == null)
            {
                return NotFound();
            }
            ViewData["carId"] = new SelectList(_context.cariler, "carId", "adres", irsaliye.carId);
            ViewData["depoId"] = new SelectList(_context.depolar, "depoId", "depoAd", irsaliye.depoId);
            ViewData["transferId"] = new SelectList(_context.depoTransferleri, "transferId", "transferId", irsaliye.transferId);
            return View(irsaliye);
        }

        // POST: irsaliyes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("irsaliyeId,irsaliyeNo,carId,irsaliyeTarihi,toplamTutar,irsaliyeTipi,aciklama,transferId,durum,depoId")] irsaliye irsaliye)
        {
            if (id != irsaliye.irsaliyeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(irsaliye);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!irsaliyeExists(irsaliye.irsaliyeId))
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
            ViewData["carId"] = new SelectList(_context.cariler, "carId", "adres", irsaliye.carId);
            ViewData["depoId"] = new SelectList(_context.depolar, "depoId", "depoAd", irsaliye.depoId);
            ViewData["transferId"] = new SelectList(_context.depoTransferleri, "transferId", "transferId", irsaliye.transferId);
            return View(irsaliye);
        }

        // GET: irsaliyes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irsaliye = await _context.irsaliyeler
                .Include(i => i.cari)
                .Include(i => i.depo)
                .Include(i => i.depoTransfer)
                .FirstOrDefaultAsync(m => m.irsaliyeId == id);
            if (irsaliye == null)
            {
                return NotFound();
            }

            return View(irsaliye);
        }

        // POST: irsaliyes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var irsaliye = await _context.irsaliyeler.FindAsync(id);
            if (irsaliye != null)
            {
                _context.irsaliyeler.Remove(irsaliye);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool irsaliyeExists(int id)
        {
            return _context.irsaliyeler.Any(e => e.irsaliyeId == id);
        }
    }
}
