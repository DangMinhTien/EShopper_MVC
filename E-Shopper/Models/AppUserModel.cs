using Microsoft.AspNetCore.Identity;

namespace E_Shopper.Models
{
    public class AppUserModel : IdentityUser
    {
        public string Address { get; set; }
    }
}
