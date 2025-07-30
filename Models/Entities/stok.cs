using DepoStok.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models.Entities
{
    public class stok
    {
        [Key]
        public int HareketId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Malzeme Numarası:")]
        public int MalzemeId { get; set; }

        [ForeignKey(nameof(MalzemeId))]
        public malzeme? Malzeme { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Depo Numarası:")]
        public int DepoId { get; set; }

        [ForeignKey(nameof(DepoId))]
        public depo? Depo { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Hareket Tarihi:")]
        public DateTime HareketTarihi { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public decimal Miktar { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Hareket Tipi:")]
        public StokHareketTipi HareketTipi { get; set; }

        [DisplayName("Evrak Numarası:")]
        public int? ReferansId { get; set; } 

        [DisplayName("Açıklama:")]
        public string? Aciklama { get; set; }

        
        public int? carId { get; set; } 

        [ForeignKey(nameof(carId))]
        public cari? cari { get; set; }


        [DisplayName("Seri Numarası:")]
        public string? SeriNo { get; set; }
    }
}
