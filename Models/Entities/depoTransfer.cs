using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    
    public class depoTransfer
    {
        [Key]
        public int transferId { get; set; }

        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        

        [DisplayName("Transfer Numarası:")]

        public int transferNo { get; set; } 

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Kaynak Depo Numarası::")]
        public int kaynakDepoId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Hedef Depo Numarası:")]
        public int hedefDepoId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Transfer Tarihi:")]
        public DateTime transferTarihi { get; set; }

        [MaxLength(500)]
        [DisplayName("Açıklama:")]
        public string? aciklama { get; set; }

        [MaxLength(100)]
        [DisplayName("Seri Numarası:")]
        public string? seriNo { get; set; }

        [ForeignKey(nameof(kaynakDepoId))]
        public depo? kaynakDepo { get; set; }

        [ForeignKey(nameof(hedefDepoId))]
        public depo? hedefDepo { get; set; }
        public ICollection<depoTransferDetay> depoTransferDetaylari { get; set; } = new List<depoTransferDetay>();

    }
}
