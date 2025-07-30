using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DepoStok.Models.Entities
{
    public class kullanici
    {
        [Key]
        public int kullaniciId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(100)]
        [DisplayName("İsim Soyisim:")]
        public string adSoyad { get; set; } = null!;

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(100)]
        [DisplayName("Mail Adres:")]
        public string email { get; set; }=null!;

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(50)]
        [MinLength(8,ErrorMessage ="Sekiz karakterden az şifre olamaz")]
        [DisplayName("Şifre:")]
        public string password { get; set; } = null!;

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Tarih:")]
        public DateTime olusturulmaTarihi { get; set; }

    }
}
