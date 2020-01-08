namespace TourAgency.DAL.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public int Term { get; set; }
        public decimal Price { get; set; }

        public HotelRoom HotelRoom { get; set; }
        public int HotelRoomId { get; set; }

        public TourFoodCategory TourFoodCategory { get; set; }
        public int TourFoodCategoryId { get; set; }
    }
}