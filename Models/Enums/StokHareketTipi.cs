using System.ComponentModel.DataAnnotations;

namespace DepoStok.Models.Enums
{
   
        public enum StokHareketTipi
        {
        [Display(Name="Stok Girişi")]
            Giris = 1,
        [Display(Name ="Stok Çıkışı")]
            Cikis = 2,
        [Display(Name ="Transfer Girişi")]
            TransferGiris = 3,
        [Display(Name ="Transfer Çıkışı")]
            TransferCikis = 4
        }
    
}
