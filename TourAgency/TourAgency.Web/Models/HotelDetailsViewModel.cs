using System.Collections.Generic;
using TourAgency.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TourAgency.Web.Models
{
    public class HotelDetailsViewModel
    {
        public Hotel Hotel { get; set; }
        public List<SelectListItem> HotelRoomNames { get; set; }
        public List<SelectListItem> FoodCategories { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }
    }
}
