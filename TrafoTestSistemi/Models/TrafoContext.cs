using Microsoft.EntityFrameworkCore;

namespace TrafoTestSistemi.Models
{
    public class TrafoContext : DbContext
    {
        public TrafoContext(DbContextOptions<TrafoContext> options) : base(options) { }

        public DbSet<TrafoTest> TestKayitlari { get; set; }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}