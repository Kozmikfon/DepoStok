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
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("❌ ModelState geçersiz:");
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            Console.WriteLine($"[ModelError] {state.Key} → {error.ErrorMessage}");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    detay.araToplam = detay.miktar * detay.birimFiyat;

                    _context.irsaliyeDetaylari.Add(detay);
                    await _context.SaveChangesAsync();

                    var irsaliye = await _context.irsaliyeler.FindAsync(detay.irsaliyeId);
                    if (irsaliye != null)
                    {
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
                    }

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
    }
}
