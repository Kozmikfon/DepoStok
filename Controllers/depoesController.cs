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
    public class depoesController : Controller
    {
        private readonly StokDbContext _context;

        public depoesController(StokDbContext context)
        {
            _context = context;
        }

        // GET: depoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.depolar.ToListAsync());
        }

        // GET: depoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depo = await _context.depolar
                .FirstOrDefaultAsync(m => m.depoId == id);
            if (depo == null)
            {
                return NotFound();
            }

            return View(depo);
        }

        // GET: depoes/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: depoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("depoId,depoAd,rafBilgisi,aciklama,konumBilgisi")] depo depo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depo);
        }

        // GET: depoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depo = await _context.depolar.FindAsync(id);
            if (depo == null)
            {
                return NotFound();
            }
            return View(depo);
        }

        // POST: depoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("depoId,depoAd,rafBilgisi,aciklama,konumBilgisi")] depo depo)
        {
            if (id != depo.depoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!depoExists(depo.depoId))
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
            return View(depo);
        }

        // GET: depoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depo = await _context.depolar
                .FirstOrDefaultAsync(m => m.depoId == id);
            if (depo == null)
            {
                return NotFound();
            }

            return View(depo);
        }

        // POST: depoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depo = await _context.depolar.FindAsync(id);
            if (depo != null)
            {
                _context.depolar.Remove(depo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool depoExists(int id)
        {
            return _context.depolar.Any(e => e.depoId == id);
        }
    }
}
