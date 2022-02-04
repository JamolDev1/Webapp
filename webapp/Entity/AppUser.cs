using Microsoft.AspNetCore.Identity;

namespace webapp.Entity;

public class AppUser : IdentityUser<Guid>
{
    public string Fullname { get; set; }
    public virtual ICollection<Organizations> Organizations { get; set; }
}