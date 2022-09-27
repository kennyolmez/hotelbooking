using ApplicationServices.DTOs;
using Domain.Entities;
using HotelAppLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Services
{
    public class HotelServices
    {
        //put the interface in here instead. 
        private readonly ApplicationDbContext db;
        public HotelServices(ApplicationDbContext _db)
        {
            db = _db;
        }


        public async Task<List<RoomTypeDTO>> GetAvailableRoomTypes(DateTime startDate, DateTime endDate, decimal minPrice, decimal maxPrice, List<int> amenityIds)
        {
            //check dates
            var bookingsWithOverlappingDates = await db.Bookings
                .Where(b => b.StartDate < endDate && startDate < b.EndDate)
                .Include(b => b.Room)
                .ToListAsync();

            var roomsAvailable = (await db.Rooms
                .Include(r => r.Type)
                .Include(r => r.Type.Amenities)
                .ToListAsync())
                .Except(bookingsWithOverlappingDates
                .Select(r => r.Room)
                .ToList());

            var roomTypes = roomsAvailable
                .Select(r => r.Type)
                .Distinct()
                .ToList();


            //check prices 
            //put in separate method?
            roomTypes = roomTypes.Where(x => (x.Price >= minPrice) && (x.Price <= maxPrice)).ToList();


            // Find better solution. For everything below
            // Populates a list of amenities based on the Ids of amenityIds passed into the method
            var amenities = await db.Amenities.Where(a => amenityIds.Contains(a.Id)).ToListAsync();
            var roomsToRemove = new List<RoomType>();
            // Make output of roomtypes only those roomtypes that contain the amenities

            foreach (var room in roomTypes)
            {
                foreach (var a in amenities)
                {
                    if (!room.Amenities.Contains(a))
                    {
                        roomsToRemove.Add(room);
                    }
                }
            }

            // RoomTypes after filtering
            roomTypes = roomTypes.Except(roomsToRemove).ToList();

            // RoomTypes ordered by price
            roomTypes = roomTypes.OrderBy(x => x.Price).ToList();

            // Convert to RoomTypeDTO
            var output = roomTypes.Select(x => new RoomTypeDTO(x))
                .ToList();

            return output;
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            // All rooms that exist in the database
            var output = await db.Rooms.ToListAsync();

            var bookings = await db.Bookings.ToListAsync();

            foreach (var booking in bookings)
            {
                var datesOverlap = booking.StartDate < endDate && startDate < booking.EndDate;

                if (datesOverlap)
                {
                    // Removes all rooms that are not available in the given date range
                    output.RemoveAll(x => x.Id == booking.RoomId);
                }
            }

            // Returns the rooms that are available
            return output;
        }

        public async Task CreateBooking(int roomTypeId,
                                  string firstName,
                                  string lastName,
                                  string email,
                                  DateTime startDate,
                                  DateTime endDate)
        {
            var roomsAvailable = await GetAvailableRooms(startDate, endDate);

            var room = roomsAvailable.Where(r => r.RoomTypeId == roomTypeId).First();

            var booking = new Bookings
            {
                RoomId = room.Id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                StartDate = startDate,
                EndDate = endDate
            };

            db.Bookings.Add(booking);
            await db.SaveChangesAsync();

            SendBookingConfirmationEmail(booking);
        }

        public async Task CreateReview(string description, int rating, string userEmail, int RoomTypeId)
        {
            var review = new Review
            {
                Description = description,
                Rating = rating,
                UserEmail = userEmail,
                RoomTypeId = RoomTypeId,
                DatePosted = DateTime.Now,
            };


            db.Reviews.Add(review);
            await db.SaveChangesAsync();
        }

        public async Task UnbookRoom(int bookingId)
        {
            var booking = await db.Bookings.Where(x => x.Id == bookingId).FirstOrDefaultAsync();

            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();
        }

        public async Task SendBookingConfirmationEmail(Bookings booking)
        {
            // Generate a token to make sure the link is secure
            Guid newId = Guid.NewGuid();
            string message = string.Format("<p>Congrulations, you have booked a room!" +
                $"</p><a href=\"https://localhost:7016/booking/unbook?bookingId={booking.Id}&token={newId}\">Unbook Room</a>");

            using (var mailMsg = new MailMessage())
            {
                mailMsg.From = new MailAddress("kenanolmezofficial@gmail.com");
                mailMsg.To.Add(booking.Email);
                mailMsg.Subject = "Booking confirmation";
                mailMsg.Body = message;
                mailMsg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("kenanolmezofficial@gmail.com", "tkdn nekc wifq remh");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mailMsg);
                }
            }
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            var amenities = await db.Amenities.ToListAsync();

            var output = amenities.Select(x => new AmenityDTO(x)).ToList();

            return output;
        }

        public async Task<RoomTypeDTO> GetRoomTypeById(int id)
        {
            var roomType = await db.RoomTypes.Where(r => r.Id == id).FirstOrDefaultAsync();

            var output = new RoomTypeDTO(roomType);

            return output;
        }

        public async Task<List<RoomTypeDTO>> GetAllRoomTypes()
        {
            var roomTypes = await db.RoomTypes.Include(a => a.Amenities).ToListAsync();

            var output = roomTypes.Select(x => new RoomTypeDTO(x)).ToList();

            return output;
        }

        public async Task<List<ReviewDTO>> GetReviewsByRoomTypeId(int RoomTypeId)
        {
            var reviews = await db.Reviews.Where(r => r.RoomTypeId == RoomTypeId).ToListAsync();

            return reviews.Select(x => new ReviewDTO(x)).ToList();
        }

        public async Task<List<ReviewDTO>> GetReviewsByUserEmail(string emailAddress)
        {
            var reviews = await db.Reviews.Where(r => r.UserEmail == emailAddress).ToListAsync();

            return reviews.Select(x => new ReviewDTO(x)).ToList();
        }

        public async Task<List<BookingsDTO>> GetBookingsByUserEmail(string emailAddress)
        {
            var bookings = await db.Bookings.Where(b => b.Email == emailAddress)
                .Include(x => x.Room)
                .Include(x => x.Room.Type)
                .ToListAsync();

            return bookings.Select(x => new BookingsDTO(x)).ToList();
        }

        public async Task<List<RoomDTO>> GetAllRooms()
        {
            return await db.Rooms.Include(rt => rt.Type).Select(x => new RoomDTO(x)).ToListAsync();
        }

    }
}
