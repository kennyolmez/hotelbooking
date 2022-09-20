using ApplicationServices.DTOs;

namespace HotelWebApp.ViewModels
{
    public class CMSRoomTypeEditViewModel
    {
        public List<string> Titles { get; set; } = new List<string>();
        public List<decimal> Prices { get; set; } = new List<decimal>();
        public List<string> Descriptions { get; set; } = new List<string>();
        public List<string> ImgUrls { get; set; } = new List<string>();
        public List<int> Sizes { get; set; } = new List<int>();
        public List<string> Amenities { get; set; } = new List<string>();
        public List<int> Ids { get; set; } = new List<int>();
        public List<RoomTypeDTO> RoomTypes { get; set; } = new List<RoomTypeDTO>();
    }
}
