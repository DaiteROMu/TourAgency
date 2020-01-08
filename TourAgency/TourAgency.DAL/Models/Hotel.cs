using System.Collections.Generic;

namespace TourAgency.DAL.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address{ get; set; }
        public string SiteUrl { get; set; }
        public string PhoneNumber { get; set; }

        public City City { get; set; }
        public int CityId { get; set; }

        public HotelCategory HotelCategory { get; set; }
        public int HotelCategoryId { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }
        public List<HotelPhoto> Photos { get; set; }
    }
}