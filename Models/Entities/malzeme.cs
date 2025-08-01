﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DepoStok.Models.Entities
{
    public class malzeme
    {
        [Key]
        public int malzemeId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Malzeme İsmi:")]
        public string malzemeAdi { get; set; } = null!;

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Birim Fiyat:")]
        public string? birim { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Kategori:")]
        public string kategori { get; set; }

        [DisplayName("Minumum Stok Miktarı:")]
        public int minStokMiktar { get; set; }
        [DisplayName("Barkod Numarası:")]
        public string? barkodNo { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Kullanım Durumu:")]
        public bool aktifPasif {  get; set; }

        [DisplayName("Açıklama:")]
        public string? aciklama { get; set; } 
    }
}
