using HotelWebApp.ViewModels;

namespace HotelWebApp.Models
{
    // Not using this
    public class RoomFilterQueryModel
    {
        public RoomFilterQueryModel(HomeIndexViewModel viewModel)
        {
            CheckInDate = viewModel.CheckInDate;
            CheckOutDate = viewModel.CheckOutDate;
            MinPrice = viewModel.MinPrice;
            MaxPrice = viewModel.MaxPrice;
        }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

    }
}
