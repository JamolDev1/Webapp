using Microsoft.AspNetCore.Identity;

namespace invoice.Entity;

public class AppUser : IdentityUser<Guid>
{
    public string Fullname { get; set; }
}