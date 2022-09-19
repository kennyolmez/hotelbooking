using ApplicationServices.DTOs;

namespace HotelWebApp.Models
{
    public class OperationsModel 
    {
        public double? RoundRatings(List<ReviewDTO> reviews, int roomTypeId)
        {

            if(reviews.Count != 0)
            {
                var output = Math.Round(reviews.Where(r => r.RoomTypeId == roomTypeId)
                            .Select(r => r.Rating)
                            .Average(), 1);

                return output;
            }

            return null;
        }
    }
}
