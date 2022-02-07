using Microsoft.AspNetCore.Identity;

namespace webapp.Entity;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
    public virtual ICollection<Organization> Organizations { get; set; }
}