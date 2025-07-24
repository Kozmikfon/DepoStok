using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class logTakip
    {
        [Key]
        public int islemId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Tablo Adı:")]
        public string tabloAdi {  get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İşlem Tipi:")]
        public string islemTipi { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İşlem Tarihi:")]
        public DateTime islemTarihi { get; set; }

        [MaxLength(500)]
        [DisplayName("İşlem Detayı:")]
        public string? detay {  get; set; }
        // Bu kullanıcı artık IdentityUser olacak
        public string kullaniciId { get; set; } = null!;

        [ForeignKey("kullaniciId")]
        public IdentityUser kullanici { get; set; } = null!;

    }
}
