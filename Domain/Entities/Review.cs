using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int RoomTypeId { get; set; }
        public string? ApplicationUserId { get; set; }
        public string UserEmail { get; set; } 
        public DateTime DatePosted { get; set; }

    }
}
