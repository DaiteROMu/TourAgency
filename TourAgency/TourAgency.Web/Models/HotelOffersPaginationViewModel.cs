using System;

namespace TourAgency.Web.Models
{
    public class HotelOffersPaginationViewModel
    {
        public int PageSize { get; } = 6;
        public int TotalNumberOfPages { get; set; }
        public int CurrentPage { get; set; }

        public HotelOffersPaginationViewModel(int count, int currentPage)
        {
            CurrentPage = currentPage;
            TotalNumberOfPages = (int)Math.Ceiling(count / (double)PageSize);
        }

        public bool IsPreviousPageButtonActive
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool IsNextPageButtonActive
        {
            get
            {
                return (CurrentPage < TotalNumberOfPages);
            }
        }
    }
}
