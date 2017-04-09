using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectApp.Models
{
    public class ProfileContext : IdentityDbContext<ApplicationUser>
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {

        }

        //public DbSet<Login> Logins { get; set; }
        //public DbSet<Register> Registration { get; set; }
    }
}
