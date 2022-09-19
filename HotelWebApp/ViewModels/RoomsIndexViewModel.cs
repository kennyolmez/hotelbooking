using ApplicationServices.DTOs;
using HotelWebApp.Models;

namespace HotelWebApp.ViewModels
{
    public class RoomsIndexViewModel
    {
        public RoomTypeDTO RoomType { get; set; }

        public double? Rating { get; set; }
        public int ReviewCount { get; set; }


    }
}
