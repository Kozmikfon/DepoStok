namespace DepoStok.Models.ViewModel
{
    public class StokHareketViewModel
    {
        public int HareketId { get; set; }
        public DateTime HareketTarihi { get; set; }
        public string DepoAd { get; set; } = string.Empty;
        public string MalzemeAdi { get; set; } = string.Empty;
        public decimal Miktar { get; set; }
        public string HareketTipi { get; set; } = string.Empty; // Enum'dan string'e çevrilecek
        public string? Aciklama { get; set; }
        public string? CariUnvan { get; set; }
        public int? ReferansId { get; set; }
        public string? SeriNo { get; set; }
    }
}
