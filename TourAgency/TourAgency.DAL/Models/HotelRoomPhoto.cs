namespace TourAgency.DAL.Models
{
    public class HotelRoomPhoto
    {
        public int Id { get; set; }
        public string BaseDirectoryName { get; set; }
        public string DirectoryName { get; set; }
        public string ImageName { get; set; }

        public HotelRoom HotelRoom { get; set; }
        public int HotelRoomId { get; set; }
    }
}