using System.Collections.Generic;

namespace TourAgency.DAL.Models
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonsNumber { get; set; }
        public int Square { get; set; }
        public string Options { get; set; }

        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }

        public HotelRoomViewCategory HotelRoomViewCategory { get; set; }
        public int HotelRoomViewCategoryId { get; set; }

        public List<HotelRoomPhoto> Photos { get; set; }
        public List<Tour> Tours { get; set; }
    }
}