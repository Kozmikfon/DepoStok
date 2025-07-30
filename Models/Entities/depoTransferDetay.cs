using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models.Entities
{
    public class depoTransferDetay
    {
        [Key]
        public int detayId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Transfer Numarası:")]
        public int transferId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Malzeme Numarası:")]
        public int malzemeId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Miktar:")]
        public decimal miktar { get; set; }

        [ForeignKey(nameof(transferId))]
        public depoTransfer? depoTransfer { get; set; }


        [ForeignKey(nameof(malzemeId))]
        public malzeme? malzeme { get; set; }

    }
}
