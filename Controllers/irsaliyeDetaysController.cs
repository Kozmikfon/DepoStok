using DepoStok.Data;
using DepoStok.Migrations;
using DepoStok.Models;
using DepoStok.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DepoStok.Controllers
{
    public class irsaliyeDetaysController : Controller
    {
        private readonly StokDbContext _context;

        public irsaliyeDetaysController(StokDbContext context)
        {
            _context = context;
        }

        // GET: irsaliyeDetays/Create
        public IActionResult Create(int? irsaliyeId = null)
        {
            ViewBag.irsaliyeId = irsaliyeId ?? 0;
            return View();
        }

        // POST: irsaliyeDetays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("irsaliyeId,malzemeId,miktar,birimFiyat,seriNo")] irsaliyeDetay detay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var irsaliye = await _context.irsaliyeler.FindAsync(detay.irsaliyeId);
                    if (irsaliye == null)
                    {
                        return NotFound();
                    }

                    // ❗ Stok çıkışıysa: yeterli stok var mı kontrol et
                    if (irsaliye.irsaliyeTipi == Models.Enums.StokHareketTipi.Cikis)
                    {
                        var mevcutStok = await _context.Vw_StokDurumu
                            .FirstOrDefaultAsync(v => v.depoId == irsaliye.depoId && v.malzemeId == detay.malzemeId);

                        if (mevcutStok == null || mevcutStok.KalanMiktar < detay.miktar)
                        {
                            TempData["stokUyarisi"] = $"UYARI: Kaynak depoda yeterli stok yok. Mevcut: {mevcutStok?.KalanMiktar ?? 0}";
                            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi", detay.malzemeId);
                            return View(detay);
                        }
                    }

                    // Devam: kayıtlar
                    detay.araToplam = detay.miktar * detay.birimFiyat;

                    _context.irsaliyeDetaylari.Add(detay);
                    await _context.SaveChangesAsync();

                    irsaliye.toplamTutar += detay.araToplam;
                    _context.irsaliyeler.Update(irsaliye);

                    var stok = new stok
                    {
                        MalzemeId = detay.malzemeId,
                        DepoId = irsaliye.depoId,
                        HareketTarihi = irsaliye.irsaliyeTarihi,
                        Miktar = detay.miktar,
                        HareketTipi = irsaliye.irsaliyeTipi,
                        ReferansId = irsaliye.irsaliyeId,
                        Aciklama = irsaliye.aciklama,
                        carId = irsaliye.carId,
                        SeriNo = detay.seriNo
                    };

                    _context.stoklar.Add(stok);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Create", new { irsaliyeId = detay.irsaliyeId });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔴 Exception: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("🔴 Inner Exception: " + ex.InnerException.Message);
            }

            return View(detay);
        }


        // GET: irsaliyeDetays
        public async Task<IActionResult> Index()
        {
            var detaylar = await _context.irsaliyeDetaylari
                .Include(d => d.irsaliye)
                .Include(d => d.malzeme)
                .ToListAsync();

            return View(detaylar);
        }

    }
}