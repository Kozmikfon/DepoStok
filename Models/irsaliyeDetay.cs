using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class irsaliyeDetay
    {
        [Key]
        public int detayId { get; set; }

        [Required]
        public int irsaliyeId { get; set; }

        [Required]
        public int malzemeId { get; set; }

        [Required]
        public decimal miktar {  get; set; }

        [Required]
        public decimal birimFiyat { get; set; }

        [Required]
        public decimal araToplam { get; set; }
        
        public string? seriNo { get; set; }

        [ForeignKey(nameof(irsaliyeId))]
        public irsaliye irsaliye { get; set; } = null!;

        [ForeignKey(nameof(malzemeId))]
        public malzeme malzeme { get; set; }
        

    }
}
