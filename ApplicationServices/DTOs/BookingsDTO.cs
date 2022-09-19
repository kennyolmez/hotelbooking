using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DTOs
{
    public class BookingsDTO
    {
        public BookingsDTO(Bookings bookings)
        {
            Id = bookings.Id;
            RoomId = bookings.RoomId;
            FirstName = bookings.FirstName;
            LastName = bookings.LastName;
            Email = bookings.Email;
            CheckInDate = bookings.StartDate;
            CheckOutDate = bookings.EndDate;
            Room = new RoomDTO(bookings.Room);
        }

        public int Id { get; set; }
        // Public RoomDTO Room { get; set; }

        public RoomDTO Room { get; set; }
       
        public int RoomId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
    }  
}
