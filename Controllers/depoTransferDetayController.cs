using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStok.Data;
using DepoStok.Models;
using DepoStok.Models.Enums;

namespace DepoStok.Controllers
{
    public class depoTransferDetayController : Controller
    {
        private readonly StokDbContext _context;

        public depoTransferDetayController(StokDbContext context)
        {
            _context = context;
        }

        // GET: DepoTransferDetay/Create
        public IActionResult Create(int transferId)
        {
            var model = new depoTransferDetay
            {
                transferId = transferId
            };

            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi");
            return View(model); // ✅ Model gönderildi
        }


        // POST: DepoTransferDetay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferId,malzemeId,miktar")] depoTransferDetay detay)
        {
            if (ModelState.IsValid)
            {
                // 1. Detay kaydını oluştur
                _context.depoTransferDetaylari.Add(detay);
                await _context.SaveChangesAsync(); // detayId oluşur

                // 2. Transfer bilgilerini al
                var transfer = await _context.depoTransferleri.FindAsync(detay.transferId);
                if (transfer != null)
                {
                    var now = DateTime.Now;

                    // 3. Kaynak depodan ÇIKIŞ
                    var cikis = new stok
                    {
                        MalzemeId = detay.malzemeId,
                        DepoId = transfer.kaynakDepoId,
                        Miktar = detay.miktar,
                        HareketTipi = StokHareketTipi.TransferCikis,
                        HareketTarihi = now,
                        ReferansId = detay.detayId,
                        Aciklama = $"Transfer çıkışı (Transfer No: {transfer.transferNo})",
                        carId=null,
                    };

                    // 4. Hedef depoya GİRİŞ
                    var giris = new stok
                    {
                        MalzemeId = detay.malzemeId,
                        DepoId = transfer.hedefDepoId,
                        Miktar = detay.miktar,
                        HareketTipi = StokHareketTipi.TransferGiris,
                        HareketTarihi = now,
                        ReferansId = detay.detayId,
                        Aciklama = $"Transfer girişi (Transfer No: {transfer.transferNo})",
                        carId=null,
                    };

                    _context.stoklar.AddRange(cikis, giris);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("details", "depoTransfer", new { id = detay.transferId });
            }

            ViewBag.transferId = detay.transferId;
            ViewBag.malzemeId = new SelectList(_context.malzemeler, "malzemeId", "malzemeAdi", detay.malzemeId);
            return View(detay);
        }
    }
}
