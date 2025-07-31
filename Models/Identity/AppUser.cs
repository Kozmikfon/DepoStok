using Microsoft.AspNetCore.Identity;

namespace DepoStok.Models.Identity
{
    public class AppUser:IdentityUser<int>
    {
        public string AdSoyad {  get; set; }=string.Empty;
        public DateTime OlusturulmaTarihi { get; set; }=DateTime.Now;
       // public ICollection<IdentityUserRole<int>> UserRoles { get; set; } = new List<IdentityUserRole<int>>();
    }

}

