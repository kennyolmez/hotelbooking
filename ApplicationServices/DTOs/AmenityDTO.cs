using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DTOs
{
    public class AmenityDTO
    {
        public AmenityDTO(Amenity amenity)
        {
            Id = amenity.Id;
            Name = amenity.Name;
            Icon = amenity.Icon;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
