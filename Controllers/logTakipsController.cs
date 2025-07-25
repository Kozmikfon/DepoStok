﻿using System;
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
    public class logTakipsController : Controller
    {
        private readonly StokDbContext _context;

        public logTakipsController(StokDbContext context)
        {
            _context = context;
        }

        // GET: logTakips
        public async Task<IActionResult> Index()
        {
            var stokDbContext = _context.logTakipler.Include(l => l.kullanici);
            return View(await stokDbContext.ToListAsync());
        }

        // GET: logTakips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logTakip = await _context.logTakipler
                .Include(l => l.kullanici)
                .FirstOrDefaultAsync(m => m.islemId == id);
            if (logTakip == null)
            {
                return NotFound();
            }

            return View(logTakip);
        }

        // GET: logTakips/Create
        public IActionResult Create()
        {
            ViewData["kullaniciId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: logTakips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("islemId,tabloAdi,islemTipi,islemTarihi,detay,kullaniciId")] logTakip logTakip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logTakip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["kullaniciId"] = new SelectList(_context.Users, "Id", "Id", logTakip.kullaniciId);
            return View(logTakip);
        }

        // GET: logTakips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logTakip = await _context.logTakipler.FindAsync(id);
            if (logTakip == null)
            {
                return NotFound();
            }
            ViewData["kullaniciId"] = new SelectList(_context.Users, "Id", "Id", logTakip.kullaniciId);
            return View(logTakip);
        }

        // POST: logTakips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("islemId,tabloAdi,islemTipi,islemTarihi,detay,kullaniciId")] logTakip logTakip)
        {
            if (id != logTakip.islemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logTakip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!logTakipExists(logTakip.islemId))
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
            ViewData["kullaniciId"] = new SelectList(_context.Users, "Id", "Id", logTakip.kullaniciId);
            return View(logTakip);
        }

        // GET: logTakips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logTakip = await _context.logTakipler
                .Include(l => l.kullanici)
                .FirstOrDefaultAsync(m => m.islemId == id);
            if (logTakip == null)
            {
                return NotFound();
            }

            return View(logTakip);
        }

        // POST: logTakips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logTakip = await _context.logTakipler.FindAsync(id);
            if (logTakip != null)
            {
                _context.logTakipler.Remove(logTakip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool logTakipExists(int id)
        {
            return _context.logTakipler.Any(e => e.islemId == id);
        }
    }
}
