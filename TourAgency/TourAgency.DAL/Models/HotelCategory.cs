using System.Collections.Generic;

namespace TourAgency.DAL.Models
{
    public class HotelCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}