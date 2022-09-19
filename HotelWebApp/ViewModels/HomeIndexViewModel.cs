using ApplicationServices.DTOs;
using Domain.Entities;
using HotelWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class HomeIndexViewModel
    {
        //convert these two to datetime somehow and validate
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public List<RoomTypeDTO> RoomTypes { get; set; } = new List<RoomTypeDTO>(); 
    }
}
