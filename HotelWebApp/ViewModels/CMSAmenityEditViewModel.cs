using ApplicationServices.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class CMSAmenityEditViewModel
    {
        public List<int> Ids { get; set; } = new List<int>();
        public List<string> Names { get; set; } = new List<string>();
        public List<string> Icons { get; set; } = new List<string>();
        public List<AmenityDTO> Amenity { get; set; } = new List<AmenityDTO>();
    }
}

