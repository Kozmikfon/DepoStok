using System;
using System.Threading.Tasks;
using MediatR;
using DepoStok.Data;
using DepoStok.Events.Domain;
using DepoStok.Models;


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
            // 1. Stok ekleniyor
            _db.stoklar.Add(s);
            await _db.SaveChangesAsync();

            // 2. İrsaliye oluşturuluyor
            var irsaliye = new irsaliye
            {
                carId = carId,
                irsaliyeTarihi = DateTime.Now,
                irsaliyeNo = "IR" + DateTime.Now.Ticks.ToString(),
                irsaliyeDetaylari = new List<irsaliyeDetay>
        {
            new irsaliyeDetay
            {
                malzemeId = s.MalzemeId,
                miktar = s.Miktar
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
