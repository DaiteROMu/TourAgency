namespace TourAgency.DAL.Models
{
    public class HotelPhoto
    {
        public int Id { get; set; }
        public string BaseDirectoryName { get; set; }
        public string DirectoryName { get; set; }
        public string ImageName { get; set; }

        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
    }
}