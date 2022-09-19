using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DTOs
{
    public class ReviewDTO
    {
        public ReviewDTO(Review review)
        {
            Id = review.Id;
            Description = review.Description;
            Rating = review.Rating;
            RoomTypeId = review.RoomTypeId;
            //ApplicationUserId = review.ApplicationUserId;
            UserEmail = review.UserEmail;
            DatePosted = review.DatePosted;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int RoomTypeId { get; set; }
        //public string? ApplicationUserId { get; set; }
        public string UserEmail { get; set; } 
        public DateTime DatePosted { get; set; }

    }
}
