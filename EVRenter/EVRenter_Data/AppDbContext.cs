using EVRenter_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRenter_Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<RentalPrice> RentalPrices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<ExtraFee> ExtraFee { get; set; }
        public DbSet<FeeType> FeeType { get; set; }
        public DbSet<HandoverAndReturn> HandoverAndReturn { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ModelImage> ModelImages { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalPrice>()
                .HasOne(rp => rp.Model)
                .WithOne(m => m.RentalPrice)
                .HasForeignKey<RentalPrice>(rp => rp.ModelID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.Id);
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Model)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(v => v.ModelID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Station)
                .WithMany(s => s.Vehicles)
                .HasForeignKey(v => v.StationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Model)
                .WithMany(m => m.Bookings)
                .HasForeignKey(v => v.ModelID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.RenterID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Voucher)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.VoucherID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HandoverAndReturn>()
                .HasKey(hr => hr.Id);
            modelBuilder.Entity<HandoverAndReturn>()
                .HasOne(hr => hr.Booking)
                .WithMany(b => b.HandoverAndReturns)
                .HasForeignKey(hr => hr.BookingID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HandoverAndReturn>()
                .HasOne(hr => hr.User)
                .WithMany(b => b.HandoverAndReturns)
                .HasForeignKey(hr => hr.StaffID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HandoverAndReturn>()
                .HasOne(hr => hr.Vehicle)
                .WithMany(b => b.HandoverAndReturns)
                .HasForeignKey(hr => hr.VehicleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExtraFee>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<ExtraFee>()
                .HasOne(p => p.User)
                .WithMany(b => b.ExtraFees)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ExtraFee>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.ExtraFees)
                .HasForeignKey(p => p.BookingID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FeeType>()
                .HasKey(ft => ft.Id);
            modelBuilder.Entity<FeeType>()
                .HasOne(p => p.ExtraFee)
                .WithMany(b => b.FeeTypes)
                .HasForeignKey(p => p.ExtraFeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasKey(f => f.Id);
            modelBuilder.Entity<Feedback>()
                .HasOne(p => p.User)
                .WithMany(b => b.Feedbacks)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>()
                .HasOne(p => p.Model)
                .WithMany(b => b.Feedbacks)
                .HasForeignKey(p => p.ModelID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ModelImage>()
                .HasKey(mi => new { mi.ModelID, mi.ImageID });
            modelBuilder.Entity<ModelImage>()
                .HasOne(mi => mi.Model)
                .WithMany(m => m.ModelImages)
                .HasForeignKey(mi => mi.ModelID);
            modelBuilder.Entity<ModelImage>()
                .HasOne(mi => mi.Image)
                .WithMany(m => m.ModelImages)
                .HasForeignKey(mi => mi.ImageID);
        }
    }
}
