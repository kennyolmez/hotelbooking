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
    public class RoomTypeDTO
    {
        public RoomTypeDTO(RoomType roomType)
        {
            Id = roomType.Id;
            Title = roomType.Title;
            Price = roomType.Price;
            Description = roomType.Description;
            ImgUrl = roomType.ImgUrl;
            Size = roomType.Size;
            Amenities = roomType.Amenities.Select(x => new AmenityDTO(x)).ToList();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int Size { get; set; }
        public List<AmenityDTO> Amenities { get; set; } = new List<AmenityDTO>();
    }
}
