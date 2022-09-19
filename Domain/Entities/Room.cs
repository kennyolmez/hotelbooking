using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomNumber { get; set; }
    }
}
