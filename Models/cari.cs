using System.ComponentModel.DataAnnotations;

namespace DepoStok.Models
{
    public class cari
    {
        [Key]
        public int carId { get; set; }

        [Required]
        [MaxLength(100)]
        public string unvan { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string telefon { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string adres { get; set; }

        [MaxLength(50)]
        public string vergiNo { get; set; }

        public string? vergiDairesi { get; set; }

    }
}
