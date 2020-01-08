using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TourAgency.Web.Models
{
    public class HotelSelectionViewModel
    {
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> HotelCategories { get; set; }
    }
}
