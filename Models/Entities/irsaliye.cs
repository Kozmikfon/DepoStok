using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models.Entities
{
    public class irsaliye
    {
        [Key]
        public int irsaliyeId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Numarası:")]
        public string irsaliyeNo { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]

        [DisplayName("Kullanıcı Numarası:")]
        public int carId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Tarihi:")]
        public DateTime irsaliyeTarihi { get; set; }= DateTime.Now;

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("toplam Tutar:")]
        public decimal toplamTutar {  get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Tipi:")]
        public Enums.StokHareketTipi irsaliyeTipi { get; set; }

        [DisplayName("Açıklama:")]
        public string? aciklama { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Durum:")]
        public bool durum {  get; set; }


        [BindNever] 
        [ForeignKey(nameof(carId))]    
        public cari? cari { get; set; }

        [Required]
        [DisplayName("Depo:")]
        public int depoId { get; set; }

        [ForeignKey(nameof(depoId))]
        public depo? depo { get; set; }


        public ICollection<irsaliyeDetay> irsaliyeDetaylari { get; set; } = new List<irsaliyeDetay>();

    }
}
