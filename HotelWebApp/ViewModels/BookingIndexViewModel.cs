using ApplicationServices.DTOs;
using FluentValidation;
using HotelWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class BookingIndexViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        
        // Need this to display which room was chosen by the user
        public RoomTypeDTO? RoomType { get; set; }
        public int RoomTypeId { get; set; }

    }

}
