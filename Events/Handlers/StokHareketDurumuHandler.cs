using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DepoStok.Data;
using DepoStok.Models;
using DepoStok.Events.Domain;

namespace DepoStok.Events.Handlers
{
    public class StokHareketDurumuHandler : INotificationHandler<StokHareketDurumu>
    {
        private readonly StokDbContext _db;

        public StokHareketDurumuHandler(StokDbContext db)
        {
            _db = db;
        }

        public async Task Handle(StokHareketDurumu evt, CancellationToken ct)
        {
            var stok = await _db.stoklar.FindAsync(new object[] { evt.HareketId }, ct);
            if (stok == null) return;

            var irs = new irsaliye
            {
                irsaliyeNo = $"IRS-{System.DateTime.UtcNow:yyyyMMddHHmmss}-{stok.HareketId}",
                carId =1, /* carId belirle */
                irsaliyeTipi = stok.HareketTipi.ToString(),
                irsaliyeTarihi = stok.HareketTarihi,
                toplamTutar = 0m,
                transferId = stok.HareketTipi.ToString().StartsWith("Transfer") ? stok.HareketId : 0,
                durum = true
            };
            await _db.irsaliyeler.AddAsync(irs, ct);
            await _db.SaveChangesAsync(ct);

            var det = new irsaliyeDetay
            {
                irsaliyeId = irs.irsaliyeId,
                malzemeId = stok.MalzemeId,
                miktar = stok.Miktar,
                birimFiyat = 0m,
                araToplam = 0m,
                seriNo = stok.SeriNo
            };
            await _db.irsaliyeDetaylari.AddAsync(det, ct);
            await _db.SaveChangesAsync(ct);
        }
    }
}
