namespace DepoStok.Models.Entities
{
    public class Vw_StokDurumu
    {
        public int depoId { get; set; }
        public string depoAd { get; set; }

        public int malzemeId { get; set; }
        public string malzemeAdi { get; set; }

        public decimal KalanMiktar { get; set; }
    }
}
