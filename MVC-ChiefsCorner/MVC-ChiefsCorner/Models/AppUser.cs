using Microsoft.AspNetCore.Identity;
using MVC_ChiefsCorner.Models.Authentication.SignUp;

namespace MVC_ChiefsCorner.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Orders = new List<Order>();
        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
