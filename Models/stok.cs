using DepoStok.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class stok
    {
        [Key]
        public int HareketId { get; set; }

        [Required]
        public int MalzemeId { get; set; }

        [ForeignKey(nameof(MalzemeId))]
        public malzeme? Malzeme { get; set; }

        [Required]
        public int DepoId { get; set; }

        [ForeignKey(nameof(DepoId))]
        public depo? Depo { get; set; }

        [Required]
        public DateTime HareketTarihi { get; set; }

        [Required]
        public int Miktar { get; set; }

        [Required]
        public StokHareketTipi HareketTipi { get; set; }

        public string? ReferansId { get; set; } 

        public string? Aciklama { get; set; }

        public string? SeriNo { get; set; }
    }
}
