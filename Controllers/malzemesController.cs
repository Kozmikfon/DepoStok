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
    public class malzemesController : Controller
    {
        private readonly StokDbContext _context;

        public malzemesController(StokDbContext context)
        {
            _context = context;
        }

        // GET: malzemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.malzemeler.ToListAsync());
        }

        // GET: malzemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var malzeme = await _context.malzemeler
                .FirstOrDefaultAsync(m => m.malzemeId == id);
            if (malzeme == null)
            {
                return NotFound();
            }

            return View(malzeme);
        }

        // GET: malzemes/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: malzemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("malzemeId,malzemeAdi,birim,kategori,minStokMiktar,barkodNo,aktifPasif,aciklama")] malzeme malzeme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(malzeme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(malzeme);
        }

        // GET: malzemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var malzeme = await _context.malzemeler.FindAsync(id);
           
            if (malzeme == null)
            {
                return NotFound();
            }
            return View(malzeme);
        }

        // POST: malzemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("malzemeId,malzemeAdi,birim,kategori,minStokMiktar,barkodNo,aktifPasif,aciklama")] malzeme malzeme)
        {
            if (id != malzeme.malzemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(malzeme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!malzemeExists(malzeme.malzemeId))
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
           
            return View(malzeme);
        }

        // GET: malzemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var malzeme = await _context.malzemeler
                .FirstOrDefaultAsync(m => m.malzemeId == id);
            if (malzeme == null)
            {
                return NotFound();
            }

            return View(malzeme);
        }

        // POST: malzemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var malzeme = await _context.malzemeler.FindAsync(id);
            if (malzeme != null)
            {
                _context.malzemeler.Remove(malzeme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool malzemeExists(int id)
        {
            return _context.malzemeler.Any(e => e.malzemeId == id);
        }
    }
}
