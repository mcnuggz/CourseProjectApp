using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectApp.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base()
        {

        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Register> Registration { get; set; }
    }
}
