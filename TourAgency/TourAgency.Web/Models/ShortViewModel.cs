using TourAgency.DAL.Models;

namespace TourAgency.Web.Models
{
    public class ShortHotelViewModel
    {
        public Hotel Hotel { get; set; }
        public decimal MinTourPrice { get; set; }
        public decimal MaxTourPrice { get; set; }
    }
}
