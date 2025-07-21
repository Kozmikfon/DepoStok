using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class depoTransfer
    {
        [Key]
        public int transferId { get; set; }

        [Required]
        [StringLength(50)]
        public string transferNo { get; set; } = null!;

        [Required]
        public int kaynakDepoId { get; set; }

        [Required]
        public int hedefDepoId { get; set; }

        [Required]
        public DateTime transferTarihi { get; set; }

        [MaxLength(500)]
        public string? aciklama { get; set; }

        [MaxLength(100)]
        public string? seriNo { get; set; }

        [ForeignKey(nameof(kaynakDepoId))]
        public depo? kaynakDepo { get; set; }

        [ForeignKey(nameof(hedefDepoId))]
        public depo? hedefDepo { get; set; }

    }
}
