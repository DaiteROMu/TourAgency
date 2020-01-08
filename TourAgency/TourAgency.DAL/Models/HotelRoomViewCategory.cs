using System.Collections.Generic;

namespace TourAgency.DAL.Models
{
    public class HotelRoomViewCategory
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }
    }
}