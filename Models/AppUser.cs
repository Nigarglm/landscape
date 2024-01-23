using Microsoft.AspNetCore.Identity;

namespace Landscape.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
