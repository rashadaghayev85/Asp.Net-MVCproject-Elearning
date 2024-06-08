using Microsoft.AspNetCore.Identity;

namespace MVCproject_Elearning.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }

}
