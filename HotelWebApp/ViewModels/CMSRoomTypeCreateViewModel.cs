using ApplicationServices.DTOs;
using HotelWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class CMSRoomTypeCreateViewModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int Size { get; set; }
        public List<string> Amenities { get; set; } = new List<string>();
        public List<RoomTypeDTO> RoomTypes { get; set; } = new List<RoomTypeDTO>();
    }
}
