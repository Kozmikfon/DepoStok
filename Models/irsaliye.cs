using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class irsaliye
    {
        [Key]
        public int irsaliyeId { get; set; }

        [Required]
        public string irsaliyeNo { get; set; }

        [Required]
        public int carId { get; set; }

        [Required]
        public DateTime irsaliyeTarihi { get; set; }

        [Required]
        public decimal toplamTutar {  get; set; }

        [Required]
        public string irsaliyeTipi { get; set; }

        public string? aciklama { get; set; }

        [Required]
        public int transferId { get; set; }

        [Required]
        public bool durum {  get; set; }

        [ForeignKey(nameof(transferId))]
        public depoTransfer depoTransfer { get; set; }

        [ForeignKey(nameof(carId))]
        public cari cari { get; set; }  


    }
}
