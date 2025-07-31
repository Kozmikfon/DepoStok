using DepoStok.Data;
using DepoStok.Models.Enums;
using DepoStok.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DepoStok.Controllers
{
    public class stokController : Controller
    {
        private readonly StokDbContext _context;

        public stokController(StokDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(StokHareketFiltreViewModel filtre)
        {
            // Tüm stokları sorguya al
            var query = _context.stoklar
                .Include(s => s.Depo)
                .Include(s => s.Malzeme)
                .Include(s => s.cari)
                .AsQueryable();

            // Filtreler
            if (filtre.BaslangicTarihi.HasValue)
                query = query.Where(s => s.HareketTarihi >= filtre.BaslangicTarihi.Value);

            if (filtre.BitisTarihi.HasValue)
                query = query.Where(s => s.HareketTarihi <= filtre.BitisTarihi.Value);

            if (filtre.DepoId.HasValue)
                query = query.Where(s => s.DepoId == filtre.DepoId);

            if (filtre.MalzemeId.HasValue)
                query = query.Where(s => s.MalzemeId == filtre.MalzemeId);

            if (!string.IsNullOrEmpty(filtre.HareketTipi))
                query = query.Where(s => s.HareketTipi.ToString() == filtre.HareketTipi);

            // DropDown verileri
            filtre.Depolar = _context.depolar
                .Select(d => new SelectListItem { Value = d.depoId.ToString(), Text = d.depoAd })
                .ToList();

            filtre.Malzemeler = _context.malzemeler
                .Select(m => new SelectListItem { Value = m.malzemeId.ToString(), Text = m.malzemeAdi })
                .ToList();

            filtre.HareketTipleri = Enum.GetNames(typeof(StokHareketTipi))
                .Select(t => new SelectListItem { Value = t, Text = t })
                .ToList();

            // Liste oluştur
            filtre.Hareketler = await query
                .OrderByDescending(s => s.HareketTarihi)
                .Select(s => new StokHareketViewModel
                {
                    HareketId = s.HareketId,
                    HareketTarihi = s.HareketTarihi,
                    DepoAd = s.Depo!.depoAd,
                    MalzemeAdi = s.Malzeme!.malzemeAdi,
                    Miktar = s.Miktar,
                    HareketTipi = s.HareketTipi.ToString(),
                    Aciklama = s.Aciklama,
                    CariUnvan = s.cari != null ? s.cari.unvan : "—",
                    ReferansId = s.ReferansId,
                    SeriNo = s.SeriNo
                })
                .ToListAsync();

            return View(filtre);
        }
    }
}
