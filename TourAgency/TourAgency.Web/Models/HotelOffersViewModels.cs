using System.Collections.Generic;

namespace TourAgency.Web.Models
{
    public class HotelOffersViewModel
    {
        public HotelOffersPaginationViewModel HotelOffersPagination { get; set; }
        public List<ShortHotelViewModel> Hotels { get; set; }
    }
}
