using DepoStok.Data;
using DepoStok.Events.Domain;
using DepoStok.Models;
using DepoStok.Models.Enums;
using MediatR;
using System;
using System.Threading.Tasks;


namespace DepoStok.Services
{
    public class StokService
    {
        private readonly StokDbContext _db;
        private readonly IMediator _mediator;
        private readonly LogService _logService;

        public StokService(StokDbContext db, IMediator mediator, LogService logService)
        {
            _db = db;
            _mediator = mediator;
            _logService = logService;
        }

        public async Task AddStokAsync(stok s, string userId, int carId)
        {
            try
            {
                _db.stoklar.Add(s);
                await _db.SaveChangesAsync();
            }// 1. Stok ekleniyor

            catch (Exception ex)
            {
                // Bunu yaz:
                throw new Exception("Stok eklenemedi: " + ex.Message, ex);
            }
            var irsaliyeTipi = s.HareketTipi switch
            {
                StokHareketTipi.Giris => "Giriş",
                StokHareketTipi.Cikis => "Çıkış",
                StokHareketTipi.TransferGiris => "Depo Transferi Girişi",
                StokHareketTipi.TransferCikis=>"Depo Transfer Çıkışı",
                _ => throw new InvalidOperationException("Bilinmeyen hareket tipi")
            };
            // 2. İrsaliye oluşturuluyor
            var irsaliye = new irsaliye
            {
                carId = carId,
                irsaliyeNo = "IR" + DateTime.Now.Ticks,
                irsaliyeTarihi = DateTime.Now,
                irsaliyeTipi = irsaliyeTipi,
                toplamTutar = 0,            // İsteğe göre hesaplanabilir
                transferId = 1,            // Gerekirse gerçek ID ile değiştirilmeli
                durum = true,
                aciklama = s.Aciklama,   // stok üzerinden açıklama geliyorsa

                irsaliyeDetaylari = new List<irsaliyeDetay>
    {
        new irsaliyeDetay
        {
            malzemeId = s.MalzemeId,
            miktar    = s.Miktar
        }
    }
            };


            _db.irsaliyeler.Add(irsaliye);
            await _db.SaveChangesAsync();

            // 3. Log yazılıyor
            await _logService.LogIslemAsync(
                userId,
                "stok",
                s.HareketTipi.ToString(),
                $"MalzemeID {s.MalzemeId} için {s.Miktar} adet {s.HareketTipi} işlemi yapıldı"
            );

            // (Opsiyonel) MediatR eventi varsa tetiklenebilir
            await _mediator.Publish(new StokHareketDurumu(s.carId.Value, userId));
        }

    }
}
