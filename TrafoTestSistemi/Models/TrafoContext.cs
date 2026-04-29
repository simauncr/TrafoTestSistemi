using Microsoft.EntityFrameworkCore;

namespace TrafoTestSistemi.Models
{
    public class TrafoContext : DbContext
    {
        public TrafoContext(DbContextOptions<TrafoContext> options) : base(options) { }

        public DbSet<TrafoTest> TestKayitlari { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Muhendis> Muhendisler { get; set; }
        public DbSet<CekirdekTipi> CekirdekTipleri { get; set; }
        public DbSet<SacCinsi> SacCinsleri { get; set; }
        public DbSet<KazanCinsi> KazanCinsleri { get; set; }
        public DbSet<YagCinsi> YagCinsleri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrafoTest>()
                .HasOne(x => x.ElektrikMuhendisi)
                .WithMany()
                .HasForeignKey(x => x.ElektrikMuhendisiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrafoTest>()
                .HasOne(x => x.MekanikMuhendisi)
                .WithMany()
                .HasForeignKey(x => x.MekanikMuhendisiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Muhendis>()
                .Property(x => x.AdSoyad)
                .HasMaxLength(450)
                .IsRequired();

            modelBuilder.Entity<Muhendis>()
                .HasIndex(x => x.AdSoyad)
                .IsUnique();

            modelBuilder.Entity<CekirdekTipi>().HasData(
                new CekirdekTipi { Id = 1, Ad = "Yuvarlak" },
                new CekirdekTipi { Id = 2, Ad = "Oval" }
            );

            modelBuilder.Entity<SacCinsi>().HasData(
                new SacCinsi { Id = 1, Ad = "M070-23P" },
                new SacCinsi { Id = 2, Ad = "M075-23P" },
                new SacCinsi { Id = 3, Ad = "M080-23P" },
                new SacCinsi { Id = 4, Ad = "M085-23P" },
                new SacCinsi { Id = 5, Ad = "M130" }
            );

            modelBuilder.Entity<KazanCinsi>().HasData(
                new KazanCinsi { Id = 1, Ad = "Dalga Duvar" },
                new KazanCinsi { Id = 2, Ad = "Düz Duvar" }
            );

            modelBuilder.Entity<YagCinsi>().HasData(
                new YagCinsi { Id = 1, Ad = "Mineral" },
                new YagCinsi { Id = 2, Ad = "Midel" },
                new YagCinsi { Id = 3, Ad = "FR3" },
                new YagCinsi { Id = 4, Ad = "Silikon" }
            );
        }
    }
}