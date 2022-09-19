using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Amenity
    {
        // This is code first convention, therefore efcore will take care of this and...
        // ...you don't need to specify which is the PK in the modelbuilder
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoomType> RoomTypes { get; set; } = new List<RoomType>();
        public string Icon { get; set; }
    }
}
