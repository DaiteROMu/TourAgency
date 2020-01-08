using Microsoft.EntityFrameworkCore;
using TourAgency.DAL.Models;
using TourAgency.DAL.Models.ModelConfigurations;

namespace TourAgency.DAL
{
    public class TourAgencyDbContext : DbContext
    {
        public TourAgencyDbContext(DbContextOptions<TourAgencyDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<HotelPhoto> HotelPhotos { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomPhoto> HotelRoomPhotos { get; set; }
        public DbSet<HotelRoomViewCategory> HotelRoomViewCategories { get; set; }
        public DbSet<TourFoodCategory> TourFoodCategories { get; set; }
        public DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.City)
                .WithMany(h => h.Hotels);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.HotelCategory)
                .WithMany(h => h.Hotels);

            modelBuilder.Entity<HotelPhoto>()
                .HasOne(p => p.Hotel)
                .WithMany(p => p.Photos);

            modelBuilder.Entity<HotelRoom>()
                .HasOne(hr => hr.Hotel)
                .WithMany(h => h.HotelRooms);

            modelBuilder.Entity<HotelRoomPhoto>()
                .HasOne(p => p.HotelRoom)
                .WithMany(hr => hr.Photos);

            modelBuilder.Entity<HotelRoom>()
                .HasOne(hcv => hcv.HotelRoomViewCategory)
                .WithMany(hr => hr.HotelRooms);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.HotelRoom)
                .WithMany(hr => hr.Tours);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.TourFoodCategory)
                .WithMany(tfc => tfc.Tours);

            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new HotelCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new HotelRoomConfiguration());
            modelBuilder.ApplyConfiguration(new HotelRoomPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new HotelRoomViewCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TourFoodCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TourConfiguration());
        }
    }
}
