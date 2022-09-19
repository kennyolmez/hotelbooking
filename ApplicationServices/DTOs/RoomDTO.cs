using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DTOs
{
    public class RoomDTO
    {
        public RoomDTO(Room room)
        {
            Id = room.Id;
            Type = new RoomTypeDTO(room.Type);
            RoomTypeId = room.RoomTypeId;
            RoomNumber = room.RoomNumber;
        }
        public int Id { get; set; }
        public RoomTypeDTO Type { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomNumber { get; set; }
    }
}
