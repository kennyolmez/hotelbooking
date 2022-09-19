using ApplicationServices.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class CMSAmenityCreateViewModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Id { get; set; }
        public List<AmenityDTO> Amenity { get; set; } = new List<AmenityDTO>();
    }

}
