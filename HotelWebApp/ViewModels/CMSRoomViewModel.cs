§using ApplicationServices.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModels
{
    public class CMSRoomViewModel
    {
        public List<int> RoomNumbers{ get; set; } = new List<int>();

        public RoomTypeDTO? Type { get; set; }
        public List<string> RoomTypes { get; set; } = new List<string>();
        [Required]
        public string RoomType { get; set; }
        [Required]
        public int RoomNumber { get; set; }

    }
}
