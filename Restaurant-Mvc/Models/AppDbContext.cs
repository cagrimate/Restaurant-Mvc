using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Malzeme> Malzemeler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<UrunMalzeme> UrunMalzemeler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Urun>().HasData(
                new Urun { Id = 1, Ad = "Şarap", Fiyat = 100, KategoriId=1}
                );
            modelBuilder.Entity<UrunMalzeme>().HasKey(t => new { t.UrunId, t.MalzemeId });
            modelBuilder.Entity<Kategori>().HasData(
                new Kategori { Id = 1, Ad = "İçecekler", }
                );
            modelBuilder.Entity<Malzeme>().HasData(
                new Malzeme { Id = 1, Ad = "Patates", }
                );
        }
    }
}
