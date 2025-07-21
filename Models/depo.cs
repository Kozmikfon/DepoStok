using System.ComponentModel.DataAnnotations;

namespace DepoStok.Models
{
    public class depo
    {
        [Key]
        public int depoId { get; set; }

        [Required]
        [StringLength(100)]
       public string depoAd {  get; set; }

        [Required]
        public string rafBilgisi { get; set; }

        [MaxLength(500)]
        public string? aciklama { get; set; }

        [Required]
        public string konumBilgisi { get; set; }


    }
}
