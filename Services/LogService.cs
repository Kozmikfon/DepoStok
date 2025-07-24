using DepoStok.Data;
using DepoStok.Models;
using System;
using System.Threading.Tasks;

namespace DepoStok.Services
{
    public class LogService
    {
        private readonly StokDbContext _db;

        public LogService(StokDbContext db)
        {
            _db = db;
        }

        public async Task LogIslemAsync(string userId, string tabloAdi, string islemTipi, string detay)
        {
            var log = new logTakip
            {
                kullaniciId = userId,
                tabloAdi = tabloAdi,
                islemTipi = islemTipi,
                islemTarihi = DateTime.Now,
                detay = detay
            };

            _db.logTakipler.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
