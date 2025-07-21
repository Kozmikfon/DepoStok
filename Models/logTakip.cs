using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class logTakip
    {
        [Key]
        public int islemId { get; set; }

        [Required]
        public int kullaniciId { get; set; }

        [Required]
        public string tabloAdi {  get; set; }

        [Required]
        public string islemTipi { get; set; }

        [Required]
        public DateTime islemTarihi { get; set; }

        [MaxLength(500)]
        public string? detay {  get; set; }

        [ForeignKey(nameof(kullaniciId))]
        public kullanici kullanici { get; set; } = null!;
    }
}
