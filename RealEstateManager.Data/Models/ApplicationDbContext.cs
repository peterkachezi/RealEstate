using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Landlord>  Landlords { get; set; }
        public virtual DbSet<Tenant>   Tenants { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<House>  Houses { get; set; }
        public virtual DbSet<Apartment>  Apartments { get; set; }
        public virtual DbSet<TenantUpload>   TenantUploads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<HospitalVisit>(entity =>
            //{
            //    entity.Property(e => e.AmountBilled).HasColumnType("decimal(18,2)");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");

            //});

            //modelBuilder.Entity<Commission>(entity =>
            //{
            //    entity.Property(e => e.CommissionAmount).HasColumnType("decimal(18,2)");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");

            //});

            seed(modelBuilder);
        }

        public static void seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<County>().HasData(
            new County { Id = 1, Name = "BOMET" },
            new County { Id = 2, Name = "BUNGOMA" },
            new County { Id = 3, Name = "BUSIA" },
            new County { Id = 4, Name = "ELGEYO/MARAKWET" },
            new County { Id = 5, Name = "EMBU" },
            new County { Id = 6, Name = "GARISSA" },
            new County { Id = 7, Name = "HOMA BAY" },
            new County { Id = 8, Name = "ISIOLO" },
            new County { Id = 9, Name = "KAJIADO" },
            new County { Id = 10, Name = "KAKAMEGA" },
            new County { Id = 11, Name = "KERICHO" },
            new County { Id = 12, Name = "KIAMBU" },
            new County { Id = 13, Name = "KILIFI" },
            new County { Id = 14, Name = "KIRINYAGA" },
            new County { Id = 15, Name = "KISII" },
            new County { Id = 16, Name = "KISUMU" },
            new County { Id = 17, Name = "KITUI" },
            new County { Id = 18, Name = "KWALE" },
            new County { Id = 19, Name = "LAIKIPIA" },
            new County { Id = 20, Name = "LAMU" },
            new County { Id = 21, Name = "MACHAKOS" },
            new County { Id = 22, Name = "MAKUENI" },
            new County { Id = 23, Name = "MANDERA" },
            new County { Id = 24, Name = "MARSABIT" },
            new County { Id = 25, Name = "MERU" },
            new County { Id = 26, Name = "MIGORI" },
            new County { Id = 27, Name = "MOMBASA" },
            new County { Id = 28, Name = "MURANGA" },
            new County { Id = 29, Name = "NAIROBI" },
            new County { Id = 30, Name = "NAKURU" },
            new County { Id = 31, Name = "NANDI" },
            new County { Id = 32, Name = "NAROK" },
            new County { Id = 33, Name = "NYAMIRA" },
            new County { Id = 34, Name = "NYANDARUA" },
            new County { Id = 35, Name = "NYERI" },
            new County { Id = 36, Name = "SAMBURU" },
            new County { Id = 37, Name = "SIAYA" },
            new County { Id = 38, Name = "TAITA TAVETA" },
            new County { Id = 39, Name = "TANA RIVER" },
            new County { Id = 40, Name = "THARAKA - NITHI" },
            new County { Id = 41, Name = "TRANS NZOIA" },
            new County { Id = 42, Name = "URKANA" },
            new County { Id = 43, Name = "UASIN GISHU" },
            new County { Id = 44, Name = "VIHIGA" },
            new County { Id = 45, Name = "WAJIR" },
            new County { Id = 46, Name = "WEST POKOT" },
            new County { Id = 47, Name = "BARINGO" }

   );
        }
    }
}
