using Microsoft.AspNetCore.Identity;

namespace MVC_ChiefsCorner.Models
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}
