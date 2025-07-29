using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class irsaliyeDetay
    {
        [Key]
        public int detayId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Numarası:")]
        public int irsaliyeId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Malzeme Numarası:")]
        public int malzemeId { get; set; }
        

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Miktar:")]
        public decimal miktar {  get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Birim Fiyat:")]
        public decimal birimFiyat { get; set; }

        
        [DisplayName("Ara Toplam:")]
        public decimal araToplam { get; set; }

        [DisplayName("Seri Numarası:")]
        public string? seriNo { get; set; }

        [BindNever]
        [ForeignKey(nameof(irsaliyeId))]
        public irsaliye? irsaliye { get; set; } = null!;

        [BindNever]
        [ForeignKey(nameof(malzemeId))]
        public malzeme? malzeme { get; set; }
        

    }
}
