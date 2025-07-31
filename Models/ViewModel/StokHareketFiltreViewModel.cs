using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepoStok.Models.ViewModel
{
    public class StokHareketFiltreViewModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int? DepoId { get; set; }
        public int? MalzemeId { get; set; }
        public string? HareketTipi { get; set; }

        public List<SelectListItem>? Depolar { get; set; }
        public List<SelectListItem>? Malzemeler { get; set; }
        public List<SelectListItem>? HareketTipleri { get; set; }

        public List<StokHareketViewModel> Hareketler { get; set; } = new();
    }
}
