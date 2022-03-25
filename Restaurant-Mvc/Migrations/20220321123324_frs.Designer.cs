﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant_Mvc.Models;

namespace Restaurant_Mvc.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220321123324_frs")]
    partial class frs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Restaurant_Mvc.Models.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategoriler");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "İçecekler"
                        });
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.Malzeme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Malzemeler");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "Patates"
                        });
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("ResimYolu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tanim")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("Urunler");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "Şarap",
                            Fiyat = 100m,
                            KategoriId = 1
                        });
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.UrunMalzeme", b =>
                {
                    b.Property<int>("UrunId")
                        .HasColumnType("int");

                    b.Property<int>("MalzemeId")
                        .HasColumnType("int");

                    b.HasKey("UrunId", "MalzemeId");

                    b.HasIndex("MalzemeId");

                    b.ToTable("UrunMalzemeler");
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.Urun", b =>
                {
                    b.HasOne("Restaurant_Mvc.Models.Kategori", "Kategori")
                        .WithMany("Urunler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.UrunMalzeme", b =>
                {
                    b.HasOne("Restaurant_Mvc.Models.Malzeme", "Malzeme")
                        .WithMany("UrunMalzemeler")
                        .HasForeignKey("MalzemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant_Mvc.Models.Urun", "Urun")
                        .WithMany("UrunMalzemeler")
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.Kategori", b =>
                {
                    b.Navigation("Urunler");
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.Malzeme", b =>
                {
                    b.Navigation("UrunMalzemeler");
                });

            modelBuilder.Entity("Restaurant_Mvc.Models.Urun", b =>
                {
                    b.Navigation("UrunMalzemeler");
                });
#pragma warning restore 612, 618
        }
    }
}
