using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class depoTransferDetay
    {
        [Key]
        public int detayId { get; set; }

        [Required]
        public int transferId { get; set; }

        [Required]
        public int malzemeId { get; set; }

        [Required]
        public decimal miktar {  get; set; }

        [ForeignKey(nameof(transferId))]
        public depoTransfer? depoTransfer { get; set; }

        [ForeignKey(nameof(malzemeId))]

        public malzeme? malzeme { get; set; }

    }
}
