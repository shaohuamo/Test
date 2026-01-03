using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SignWithGoogle.DbContext
{
    public class AppDbContext:IdentityDbContext<IdentityUser<Guid>,
        IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
