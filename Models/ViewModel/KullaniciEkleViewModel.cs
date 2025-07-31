using System.ComponentModel.DataAnnotations;

namespace DepoStok.Models.ViewModel
{
    public class KullaniciEkleViewModel
    {
        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string AdSoyad { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; } = "";

        [Required]
        public string Rol { get; set; } = "";
    }

}
