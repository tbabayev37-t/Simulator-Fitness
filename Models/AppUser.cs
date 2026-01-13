using Microsoft.AspNetCore.Identity;

namespace Simulator16TB.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}
