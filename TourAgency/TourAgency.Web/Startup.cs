using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TourAgency.DAL;
using TourAgency.DAL.Models;

namespace TourAgency.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddEntityFrameworkNpgsql().AddDbContext<TourAgencyDbContext>(options => options.UseNpgsql(ConfigurationExtensions.GetConnectionString(Configuration, "DefaultConnection"), b => b.MigrationsAssembly("TourAgency.Web")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                TourAgencyDbContext context = serviceScope.ServiceProvider.GetRequiredService<TourAgencyDbContext>();
                context.Database.Migrate();
                if (context.Countries.ToList().Count == 0)
                {
                    FillDatabaseTables(context);
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        private void FillDatabaseTables(TourAgencyDbContext context)
        {
            context.Countries.Add(new Country { Name = "Belarus" });
            context.Countries.Add(new Country { Name = "Russia" });
            context.Countries.Add(new Country { Name = "Egypt" });
            context.Countries.Add(new Country { Name = "Turkey" });
            context.Countries.Add(new Country { Name = "Cuba" });
            context.Countries.Add(new Country { Name = "Spain" });
            context.SaveChanges();

            context.HotelCategories.Add(new HotelCategory { Name = "1*" });
            context.HotelCategories.Add(new HotelCategory { Name = "2*" });
            context.HotelCategories.Add(new HotelCategory { Name = "3*" });
            context.HotelCategories.Add(new HotelCategory { Name = "4*" });
            context.HotelCategories.Add(new HotelCategory { Name = "5*" });
            context.HotelCategories.Add(new HotelCategory { Name = "HV-1" });
            context.HotelCategories.Add(new HotelCategory { Name = "HV-2" });
            context.HotelCategories.Add(new HotelCategory { Name = "APT" });
            context.SaveChanges();

            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "RO", FullName = "Room Only" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "BB", FullName = "Bed & Breakfast" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "HB", FullName = "Half Board" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "HB+", FullName = "Half Board Plus" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "FB", FullName = "Full Board" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "FB+", FullName = "Full Board Plus" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "AI", FullName = "All Inclusive" });
            context.TourFoodCategories.Add(new TourFoodCategory { ShortName = "UAI", FullName = "Ultra All Inclusive" });
            context.SaveChanges();

            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "PV", FullName = "Pool View" });
            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "PaV", FullName = "Park View" });
            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "SV", FullName = "Sea View" });
            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "CV", FullName = "City View" });
            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "MV", FullName = "Mountain View" });
            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "OV", FullName = "Ocean View" });
            context.HotelRoomViewCategories.Add(new HotelRoomViewCategory { ShortName = "RoH", FullName = "Run of House" });
            context.SaveChanges();

            context.Cities.Add(new City { CountryId = 1, Name = "Minsk" });
            context.Cities.Add(new City { CountryId = 1, Name = "Naroch" });
            context.Cities.Add(new City { CountryId = 2, Name = "Sochi" });
            context.Cities.Add(new City { CountryId = 2, Name = "Yalta" });
            context.Cities.Add(new City { CountryId = 3, Name = "Sharm-el-sheikh" });
            context.Cities.Add(new City { CountryId = 3, Name = "Hurgada" });
            context.Cities.Add(new City { CountryId = 3, Name = "Alexandria" });
            context.Cities.Add(new City { CountryId = 4, Name = "Antalya" });
            context.Cities.Add(new City { CountryId = 4, Name = "Alanya" });
            context.Cities.Add(new City { CountryId = 5, Name = "Havana" });
            context.Cities.Add(new City { CountryId = 6, Name = "Barcelona" });
            context.SaveChanges();

            context.Hotels.Add(new Hotel { Name = "Belarus", Description = "Хороший отель в центре Минска", Address = "Nezavisimosti 11", SiteUrl = "belarushotel.by", PhoneNumber = "+125621512", CityId = 1, HotelCategoryId = 4 });
            context.Hotels.Add(new Hotel { Name = "Europe", Description = "Престижнейший отель в центре Минска", Address = "Pobedy 22", SiteUrl = "europehotel.by", PhoneNumber = "+125621512", CityId = 1, HotelCategoryId = 5 });
            context.Hotels.Add(new Hotel { Name = "Naroch", Description = "Отель на берегу красивого Озера", Address = "Centralnaya 3", SiteUrl = null, PhoneNumber = "+125621512", CityId = 2, HotelCategoryId = 3 });
            context.Hotels.Add(new Hotel { Name = "Roza Vetrov", Description = "Отель на берегу", Address = "Morskaya 3", SiteUrl = null, PhoneNumber = "+125621512", CityId = 3, HotelCategoryId = 2 });
            context.Hotels.Add(new Hotel { Name = "Levant", Description = null, Address = "Krymskaya 7", SiteUrl = null, PhoneNumber = "+125621512", CityId = 4, HotelCategoryId = 3 });
            context.Hotels.Add(new Hotel { Name = "Sheraton Sharm Main", Description = "Good Hotel", Address = "Al Pasha Coast, Qesm Sharm Ash Sheikh, South Sinai Governorate", SiteUrl = null, PhoneNumber = "+125621512", CityId = 5, HotelCategoryId = 5 });
            context.Hotels.Add(new Hotel { Name = "Rehana Sharm Resort", Description = "Nice Hotel", Address = "Al Pasha Coast, Qesm Sharm Ash Sheikh, East Sinai Governorate", SiteUrl = null, PhoneNumber = "+125621512", CityId = 5, HotelCategoryId = 4 });
            context.Hotels.Add(new Hotel { Name = "Beach Albatros", Description = "Good", Address = "Hurgada", SiteUrl = null, PhoneNumber = "+125621512", CityId = 6, HotelCategoryId = 4 });
            context.Hotels.Add(new Hotel { Name = "Iberotel Borg El Arab", Description = null, Address = "Alexandria", SiteUrl = null, PhoneNumber = "+125621512", CityId = 7, HotelCategoryId = 7 });
            context.Hotels.Add(new Hotel { Name = "Royal Holiday Palace", Description = null, Address = "Antalya", SiteUrl = null, PhoneNumber = "+125621512", CityId = 8, HotelCategoryId = 4 });
            context.Hotels.Add(new Hotel { Name = "Rixos Downtown", Description = "Good Hotel", Address = "Antalya", SiteUrl = null, PhoneNumber = "+125621512", CityId = 8, HotelCategoryId = 5 });
            context.Hotels.Add(new Hotel { Name = "Vikingen Hotel", Description = "Good Hotel", Address = "Alanya", SiteUrl = null, PhoneNumber = "+125621512", CityId = 9, HotelCategoryId = 5 });
            context.Hotels.Add(new Hotel { Name = "Inglaterra", Description = null, Address = "Havana", SiteUrl = null, PhoneNumber = "+125621512", CityId = 10, HotelCategoryId = 6 });
            context.Hotels.Add(new Hotel { Name = "W Barcelona", Description = "5 звезд на берегу океана", Address = "Barcelona", SiteUrl = null, PhoneNumber = "+125621512", CityId = 11, HotelCategoryId = 8 });
            context.SaveChanges();

            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Belarus_Minsk_Belarus", ImageName = "BelarusHotel.jpg", HotelId = 1 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Belarus_Minsk_Belarus", ImageName = "full_territory_view.jpg", HotelId = 1 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Belarus_Minsk_Belarus", ImageName = "full_hotel_view.jpg", HotelId = 1 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Belarus_Minsk_Europe", ImageName = "EuropeHotel.jpg", HotelId = 2 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Belarus_Naroch_Naroch", ImageName = "NarochHotel.jpg", HotelId = 3 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Russia_Yalta_RozaVetrov", ImageName = "РозаВетров.jpg", HotelId = 4 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Russia_Yalta_Levant", ImageName = "ЛивантЯлта.jpg", HotelId = 5 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Egypt_Sharm-el-sheikh_SheratonSharmMain", ImageName = "SheratonSharmMain.png", HotelId = 6 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Egypt_Sharm-el-sheikh_RehanaSharmResort", ImageName = "RehanaSharmResort.jpg", HotelId = 7 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Egypt_Hurgada_BeachAlbatros", ImageName = "BeachAlbatros.jpg", HotelId = 8 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Egypt_Alexandria_IberotelBorgElArab", ImageName = "IberotelBorgElArab.jpg", HotelId = 9 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Turkey_Antalya_RoyalHolidayPalace", ImageName = "RoyalHolidayPalace.jpg", HotelId = 10 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Turkey_Antalya_RixosDowntown", ImageName = "RixosDowntown.jpg", HotelId = 11 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Turkey_Alanya_VikingenHotel", ImageName = "VikingenHotel.jpg", HotelId = 12 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Cuba_Havana_Inglaterra", ImageName = "Inglaterra.jpg", HotelId = 13 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Spain_Barcelona_WBarcelona", ImageName = "WBarcelona.jpeg", HotelId = 14 });
            context.HotelPhotos.Add(new HotelPhoto { BaseDirectoryName = @"images\Hotels\", DirectoryName = "Spain_Barcelona_WBarcelona", ImageName = "full_view.jpg", HotelId = 14 });
            context.SaveChanges();

            context.HotelRooms.Add(new HotelRoom { Name = "Comfort 1", PersonsNumber = 1, Square = 20, Options = "Wi-Fi, Shower and etc.", HotelId = 1, HotelRoomViewCategoryId = 4 });
            context.HotelRooms.Add(new HotelRoom { Name = "Comfort 2", PersonsNumber = 2, Square = 30, Options = "Wi-Fi, Shower and etc.", HotelId = 1, HotelRoomViewCategoryId = 4 });
            context.HotelRooms.Add(new HotelRoom { Name = "Comfort Premium", PersonsNumber = 2, Square = 30, Options = "All possible options", HotelId = 1, HotelRoomViewCategoryId = 4 });
            context.SaveChanges();

            context.HotelRoomPhotos.Add(new HotelRoomPhoto { BaseDirectoryName = @"images\Hotels\Belarus_Minsk_Belarus\Rooms", DirectoryName = "Comfort1", ImageName = "one_bad.jpg", HotelRoomId = 1 });
            context.HotelRoomPhotos.Add(new HotelRoomPhoto { BaseDirectoryName = @"images\Hotels\Belarus_Minsk_Belarus\Rooms", DirectoryName = "Comfort1", ImageName = "bath.jpg", HotelRoomId = 1 });
            context.HotelRoomPhotos.Add(new HotelRoomPhoto { BaseDirectoryName = @"images\Hotels\Belarus_Minsk_Belarus\Rooms", DirectoryName = "Comfort2", ImageName = "view.jpg", HotelRoomId = 2 });
            context.HotelRoomPhotos.Add(new HotelRoomPhoto { BaseDirectoryName = @"images\Hotels\Belarus_Minsk_Belarus\Rooms", DirectoryName = "Comfort2", ImageName = "view2.jpg", HotelRoomId = 2 });
            context.HotelRoomPhotos.Add(new HotelRoomPhoto { BaseDirectoryName = @"images\Hotels\Belarus_Minsk_Belarus\Rooms", DirectoryName = "Comfort2", ImageName = "bath.jpg", HotelRoomId = 2 });
            context.HotelRoomPhotos.Add(new HotelRoomPhoto { BaseDirectoryName = @"images\Hotels\Belarus_Minsk_Belarus\Rooms", DirectoryName = "ComfortPremium", ImageName = "view.jpg", HotelRoomId = 3 });
            context.SaveChanges();

            context.Tours.Add(new Tour { Term = 3, Price = 100, HotelRoomId = 1, TourFoodCategoryId = 1 });
            context.Tours.Add(new Tour { Term = 3, Price = 200, HotelRoomId = 1, TourFoodCategoryId = 5 });
            context.Tours.Add(new Tour { Term = 7, Price = 600, HotelRoomId = 1, TourFoodCategoryId = 7 });
            context.Tours.Add(new Tour { Term = 2, Price = 300, HotelRoomId = 2, TourFoodCategoryId = 2 });
            context.Tours.Add(new Tour { Term = 2, Price = 400, HotelRoomId = 2, TourFoodCategoryId = 3 });
            context.Tours.Add(new Tour { Term = 2, Price = 800, HotelRoomId = 3, TourFoodCategoryId = 7 });
            context.SaveChanges();
        }
    }
}
