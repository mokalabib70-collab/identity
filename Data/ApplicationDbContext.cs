using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Proctor> Proctors { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Admin> Admins { get; set; }


        public DbSet<LoginLog> loginLogs { get; set; }
        
    }
}
