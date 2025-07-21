using System.ComponentModel.DataAnnotations;

namespace DepoStok.Models
{
    public class kullanici
    {
        [Key]
        public int kullaniciId { get; set; }

        [Required]
        [MaxLength(100)]
        public string adSoyad { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string email { get; set; }=null!;

        [Required]
        [MaxLength(100)]
        public string password { get; set; } = null!;

        [Required]
        public DateTime olusturulmaTarihi { get; set; }

    }
}
