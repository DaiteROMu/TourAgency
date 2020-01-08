using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourAgency.DAL;
using TourAgency.DAL.Models;
using TourAgency.Web.Models;

namespace TourAgency.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly TourAgencyDbContext _context;

        public HomeController(TourAgencyDbContext context)
        {
            _context = context;
            _context.Countries.ToList();
            _context.Cities.ToList();
            _context.Hotels.ToList();
            _context.HotelCategories.ToList();
            _context.HotelPhotos.ToList();
            _context.HotelRoomViewCategories.ToList();
            _context.HotelRooms.ToList();
            _context.HotelRoomPhotos.ToList();
            _context.Tours.ToList();
            _context.TourFoodCategories.ToList();
        }

        public IActionResult Index()
        {
            List<SelectListItem> countries = new List<SelectListItem>
            {
                new SelectListItem("Choose country", "-1", true, true),
                new SelectListItem("All Countries", "0")
            };
            foreach (Country country in _context.Countries.ToList())
            {
                countries.Add(new SelectListItem(country.Name, country.Id.ToString()));
            }

            List<SelectListItem> cities = new List<SelectListItem>
            {
                new SelectListItem("Choose city", "-1", true, true),
                new SelectListItem("All Cities", "0")
            };

            List<SelectListItem> hotelCategories = new List<SelectListItem>();
            foreach (HotelCategory category in _context.HotelCategories.ToList())
            {
                hotelCategories.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }

            HotelOffersPaginationViewModel paginationViewModel = new HotelOffersPaginationViewModel(_context.Hotels.ToList().Count, 1);

            List<ShortHotelViewModel> hotels = new List<ShortHotelViewModel>();

            foreach (Hotel hotel in _context.Hotels.ToList())
            {
                List<decimal> prices = new List<decimal>();
                foreach (Tour tour in _context.Tours.Where(t => t.HotelRoom.HotelId == hotel.Id).ToList())
                {
                    prices.Add(tour.Price);
                }
                ShortHotelViewModel item = new ShortHotelViewModel
                {
                    Hotel = hotel,
                    MinTourPrice = prices.Count > 0 ? prices.Min() : 0,
                    MaxTourPrice = prices.Count > 0 ? prices.Max() : 0
                };
                hotels.Add(item);
            }

            MainViewModel model = new MainViewModel
            {
                HotelSelection = new HotelSelectionViewModel
                {
                    Countries = countries,
                    Cities = cities,
                    HotelCategories = hotelCategories
                },
                HotelOffers = new HotelOffersViewModel
                {
                    HotelOffersPagination = paginationViewModel,
                    Hotels = hotels
                }
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetCitiesByCountryId(int countryId)
        {
            List<SelectListItem> cities = new List<SelectListItem>
            {
                new SelectListItem("Choose city", "-1", true, true),
                new SelectListItem("All Cities", "0")
            };

            foreach (City city in _context.Cities.Where(c => c.CountryId == countryId).ToList())
            {
                cities.Add(new SelectListItem(city.Name, city.Id.ToString()));
            }

            return PartialView("_CitySelection", cities);
        }

        [HttpGet]
        public IActionResult GetHotelsFromSelection(int countryId, int cityId, string selectedHotelCategories, int page)
        {
            char[] sep = new char[] { ';' };
            List<int> selectedHotelCategoriesList = new List<int>();
            if (selectedHotelCategories != null)
            {
                foreach (string item in selectedHotelCategories.Trim().Split(sep))
                {
                    selectedHotelCategoriesList.Add(int.Parse(item));
                }
            }

            List<Hotel> hotels = _context.Hotels.ToList();

            if (countryId > 0)
            {
                hotels = hotels.Where(h => h.City.CountryId == countryId).ToList();
            }
            if (cityId > 0)
            {
                hotels = hotels.Where(h => h.CityId == cityId).ToList();
            }
            if (selectedHotelCategoriesList.Count > 0)
            {
                hotels = hotels.Where(h => selectedHotelCategoriesList.Contains(h.HotelCategoryId)).ToList();
            }

            List<ShortHotelViewModel> hotelsModel = new List<ShortHotelViewModel>();

            foreach (Hotel hotel in hotels)
            {
                List<decimal> prices = new List<decimal>();
                foreach (Tour tour in _context.Tours.Where(t => t.HotelRoom.HotelId == hotel.Id).ToList())
                {
                    prices.Add(tour.Price);
                }
                ShortHotelViewModel item = new ShortHotelViewModel
                {
                    Hotel = hotel,
                    MinTourPrice = prices.Count > 0 ? prices.Min() : 0,
                    MaxTourPrice = prices.Count > 0 ? prices.Max() : 0
                };
                hotelsModel.Add(item);
            }

            HotelOffersViewModel model = new HotelOffersViewModel
            {
                Hotels = hotelsModel,
                HotelOffersPagination = new HotelOffersPaginationViewModel(hotelsModel.Count, page)
            };

            return ViewComponent("HotelOffers", new { hotelOffersViewModel = model });
        }

        [HttpGet]
        public IActionResult GetHotelById(int hotelId)
        {
            List<SelectListItem> hotelRoomNames = new List<SelectListItem>();
            foreach (HotelRoom hotelRoom in _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToList())
            {
                hotelRoomNames.Add(new SelectListItem(hotelRoom.Name, hotelRoom.Id.ToString()));
            }

            List<SelectListItem> foodCategories = new List<SelectListItem>();
            foreach (TourFoodCategory foodCategory in _context.TourFoodCategories.Where(fc => _context.Tours.Where(t => t.HotelRoom.HotelId == hotelId).Select(t => t.TourFoodCategory).Contains(fc)).ToList())
            {
                foodCategories.Add(new SelectListItem(string.Join(' ', foodCategory.ShortName, foodCategory.FullName), foodCategory.Id.ToString()));
            }

            HotelDetailsViewModel model = new HotelDetailsViewModel
            {
                Hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId),
                HotelRooms = _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToList(),
                FoodCategories = foodCategories,
                HotelRoomNames = hotelRoomNames
            };

            return PartialView("_HotelDetails", model);
        }

        [HttpGet]
        public IActionResult GetToursFromSelection(int hotelId, string selectedHotelRoomNames, string selectedFoodCategories)
        {
            char[] sep = new char[] { ';' };
            List<int> selectedHotelRoomNamesList = new List<int>();
            if (selectedHotelRoomNames != null)
            {
                foreach (string item in selectedHotelRoomNames.Trim().Split(sep))
                {
                    selectedHotelRoomNamesList.Add(int.Parse(item));
                }
            }
            List<int> selectedFoodCategoriesList = new List<int>();
            if (selectedFoodCategories != null)
            {
                foreach (string item in selectedFoodCategories.Trim().Split(sep))
                {
                    selectedFoodCategoriesList.Add(int.Parse(item));
                }
            }

            List<HotelRoom> model = _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToList();

            if (selectedHotelRoomNamesList.Count > 0)
            {
                model = model.Where(hr => selectedHotelRoomNamesList.Contains(hr.Id)).ToList();
            }

            if (selectedFoodCategoriesList.Count > 0)
            {
                foreach (HotelRoom hotelRoom in model)
                {
                    hotelRoom.Tours = hotelRoom.Tours.Where(t => selectedFoodCategoriesList.Contains(t.TourFoodCategoryId)).ToList();
                }
            }

            return PartialView("_HotelRoomOffers", model);
        }

        [HttpGet]
        public IActionResult GetHotelRoomById(int hotelRoomId)
        {
            return PartialView("_HotelRoomDetails", _context.HotelRooms.FirstOrDefault(hr => hr.Id == hotelRoomId));
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
