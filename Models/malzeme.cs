using System.ComponentModel.DataAnnotations;


namespace DepoStok.Models
{
    public class malzeme

    {
        [Key]
        public int malzemeId { get; set; }

        [Required]
        public string malzemeAdi { get; set; } = null!;
        public string? birim { get; set; }
        public string? kategori { get; set; }
        public int minStokMiktar { get; set; }
        public string? barkodNo { get; set; }
        public bool aktifPasif {  get; set; }
        public string? aciklama { get; set; } 

    }
}
