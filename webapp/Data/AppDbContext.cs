using invoice.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace invoice.Data;

public class AppDbContext : IdentityDbContext<
                                    AppUser, 
                                    AppRole, 
                                    Guid, 
                                    IdentityUserClaim<Guid>, 
                                    AppUserRole, 
                                    IdentityUserLogin<Guid>, 
                                    IdentityRoleClaim<Guid>, 
                                    IdentityUserToken<Guid>>
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

 
}