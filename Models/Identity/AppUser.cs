using Microsoft.AspNetCore.Identity;

namespace DepoStok.Models.Identity
{
    public class AppUser:IdentityUser<int>
    {
        public string AdSoyad {  get; set; }=string.Empty;
        public DateTime OlusturulmaTarihi { get; set; }=DateTime.Now;   

    }
}
