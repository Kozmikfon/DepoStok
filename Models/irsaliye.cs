using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStok.Models
{
    public class irsaliye
    {
        [Key]
        public int irsaliyeId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Numarası:")]
        public string irsaliyeNo { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]

        [DisplayName("Kullanıcı Numarası:")]
        public int carId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Tarihi:")]
        public DateTime irsaliyeTarihi { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("toplam Tutar:")]
        public decimal toplamTutar {  get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("İrsaliye Tipi:")]
        public string irsaliyeTipi { get; set; }

        [DisplayName("Açıklama:")]
        public string? aciklama { get; set; }

        
        [DisplayName("Transfer Numarası:")]
        public int? transferId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Durum:")]
        public bool durum {  get; set; }

        [ForeignKey(nameof(transferId))]
        public depoTransfer? depoTransfer { get; set; }

        [ForeignKey(nameof(carId))]
        public cari cari { get; set; }

        public ICollection<irsaliyeDetay> irsaliyeDetaylari { get; set; } = new List<irsaliyeDetay>();

    }
}
