using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace invoice.Data;
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }
}