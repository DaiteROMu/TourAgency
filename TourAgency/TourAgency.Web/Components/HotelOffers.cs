using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TourAgency.Web.Models;

namespace TourAgency.Web.Components
{
    public class HotelOffers : ViewComponent
    {
        public IViewComponentResult Invoke(HotelOffersViewModel hotelOffersViewModel)
        {
            hotelOffersViewModel.Hotels = hotelOffersViewModel.Hotels.Skip((hotelOffersViewModel.HotelOffersPagination.CurrentPage - 1) * hotelOffersViewModel.HotelOffersPagination.PageSize).Take(hotelOffersViewModel.HotelOffersPagination.PageSize).ToList();

            return View("_HotelOffers", hotelOffersViewModel);
        }
    }
}
