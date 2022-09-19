using ApplicationServices.DTOs;
using HotelWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class RoomsInfoViewModel
    {
        public RoomTypeDTO RoomType { get; set; }
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

        public int RoomTypeId { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string UserEmail { get; set; }
    }
}
