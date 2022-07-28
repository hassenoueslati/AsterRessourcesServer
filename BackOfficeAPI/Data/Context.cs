using BackOfficeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidature>().HasKey(c => new { c.OffreFK, c.CandidatFK });
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
