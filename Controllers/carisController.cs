using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStok.Data;
using DepoStok.Models.Entities;

namespace DepoStok.Controllers
{
    public class carisController : Controller
    {
        private readonly StokDbContext _context;

        public carisController(StokDbContext context)
        {
            _context = context;
        }

        // GET: caris
        public async Task<IActionResult> Index()
        {
            return View(await _context.cariler.ToListAsync());
        }

        // GET: caris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cari = await _context.cariler
                .FirstOrDefaultAsync(m => m.carId == id);
            if (cari == null)
            {
                return NotFound();
            }

            return View(cari);
        }

        // GET: caris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: caris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("carId,unvan,telefon,email,adres,vergiNo,vergiDairesi")] cari cari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cari);
        }

        // GET: caris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cari = await _context.cariler.FindAsync(id);
            if (cari == null)
            {
                return NotFound();
            }
            return View(cari);
        }

        // POST: caris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("carId,unvan,telefon,email,adres,vergiNo,vergiDairesi")] cari cari)
        {
            if (id != cari.carId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cariExists(cari.carId))
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
            return View(cari);
        }

        // GET: caris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cari = await _context.cariler
                .FirstOrDefaultAsync(m => m.carId == id);
            if (cari == null)
            {
                return NotFound();
            }

            return View(cari);
        }

        // POST: caris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cari = await _context.cariler.FindAsync(id);
            if (cari != null)
            {
                _context.cariler.Remove(cari);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cariExists(int id)
        {
            return _context.cariler.Any(e => e.carId == id);
        }

        //carigetir
        [HttpGet]
        public IActionResult GetCariBilgi(int id)
        {
            var cari = _context.cariler
                .Where(c => c.carId == id)
                .Select(c => new
                {
                    c.unvan,
                    c.adres,
                    c.telefon,
                    c.vergiNo
                })
                .FirstOrDefault();

            if (cari == null)
                return NotFound();

            return Json(cari);
        }

    }
}
