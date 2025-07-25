using DepoStok.Data;
using DepoStok.Events.Domain;
using DepoStok.Models;
using DepoStok.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                // 1. Stok ekleniyor
                _db.stoklar.Add(s);
                await _db.SaveChangesAsync();

                // 2. İrsaliye tipi belirleniyor
                string irsaliyeTipi = s.HareketTipi switch
                {
                    StokHareketTipi.Giris => "Giriş",
                    StokHareketTipi.Cikis => "Çıkış",
                    StokHareketTipi.TransferGiris => "Transfer Girişi",
                    StokHareketTipi.TransferCikis=>"Transfer Çıkışı",
                    _ => "Bilinmiyor"
                };

                // 3. TransferId sadece transferse atanır
                int? transferId = null;
                if (s.HareketTipi == StokHareketTipi.TransferCikis)
                {
                    // Transfer varsa al
                    var transfer = await _db.depoTransferleri.FirstOrDefaultAsync();
                    if (transfer == null)
                        throw new Exception("Depo Transfer işlemi için transfer kaydı bulunamadı.");
                    transferId = transfer.transferId;
                }

                // 4. İrsaliye oluşturuluyor
                var irsaliye = new irsaliye
                {
                    carId = carId,
                    irsaliyeNo = "IR" + DateTime.Now.Ticks,
                    irsaliyeTarihi = DateTime.Now,
                    irsaliyeTipi = irsaliyeTipi,
                    toplamTutar = 0,
                    transferId = transferId, // sadece transferse atanır
                    durum = true,
                    irsaliyeDetaylari = new List<irsaliyeDetay>
            {
                new irsaliyeDetay
                {
                    malzemeId = s.MalzemeId,
                    miktar = s.Miktar,
                    birimFiyat = 0,
                    araToplam = 0,
                    seriNo = s.SeriNo
                }
            }
                };

                _db.irsaliyeler.Add(irsaliye);
                await _db.SaveChangesAsync();

                // 5. Log yazılıyor
                var log = new logTakip
                {
                    kullaniciId = userId,
                    islemTipi = "Stok Ekleme",
                    tabloAdi = "stok",
                    detay = $"MalzemeId={s.MalzemeId}, Miktar={s.Miktar}, Tip={s.HareketTipi}",
                    islemTarihi = DateTime.Now
                };

                _db.logTakipler.Add(log);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA: " + ex.Message);
                throw;
            }
        }



    }
}
