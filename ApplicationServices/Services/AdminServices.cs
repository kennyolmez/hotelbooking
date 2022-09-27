using Domain.Entities;
using HotelAppLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Services
{
    public class AdminServices
    {
        private readonly ApplicationDbContext db;
        public AdminServices(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task CreateAmenity(string name, string icon)
        {
            var amenity = new Amenity
            {
                Name = name,
                Icon = icon
            };

            db.Add(amenity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAmenity(int id)
        {
            var amenity = await db.Amenities.Where(x => x.Id == id).FirstOrDefaultAsync();

            db.Remove(amenity);
            await db.SaveChangesAsync();
        }

        public async Task EditAmenity(string name, string icon, int id)
        {
            var amenity = await db.Amenities.Where(x => x.Id == id).FirstOrDefaultAsync();

            amenity.Name = name;
            amenity.Icon = icon;

            await db.SaveChangesAsync();
        }

        public async Task CreateRoomType(string title, decimal price, string description, string imgUrl, int size, List<string> amenities)
        {

            var roomType = new RoomType
            {
                Title = title,
                Price = price,
                Description = description,
                ImgUrl = imgUrl,
                Size = size,
                Amenities = await db.Amenities.Where(a => amenities.Contains(a.Name)).ToListAsync()
            };

           db.Add(roomType);
            await db.SaveChangesAsync();
        }

        public async Task DeleteRoomType(int id)
        {
            var roomType = await db.RoomTypes.Where(x => x.Id == id).FirstOrDefaultAsync();

            await DeleteReview(roomType.Id);

            db.Remove(roomType);
            await db.SaveChangesAsync();
        }

        public async Task EditRoomType(string title, decimal price, string description, string imgUrl, int size, List<string> amenity, int id)
        {
            var roomType = await db.RoomTypes.Where(x => x.Id == id)
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync();

            // Make a method of this? 
            roomType.Title = title;
            roomType.Description = description;
            roomType.Size = size;
            roomType.ImgUrl = imgUrl;
            roomType.Price = price;

            // Another cheap solution, but it works. Makes sure that the list is not empty for that room, otherwise it will clear all the rooms of amenities.

            if(amenity.Count() > 0)
            {
                roomType.Amenities.Clear();
                roomType.Amenities = await db.Amenities.Include(r => r.RoomTypes).Where(x => amenity.Contains(x.Name)).ToListAsync();
            }
   
            
            
            await db.SaveChangesAsync();
        }

        public async Task CreateRoom(string roomType, int roomNumber)
        {
            var room = new Room
            {
                RoomTypeId = await db.RoomTypes.Where(x => x.Title == roomType).Select(x => x.Id).FirstOrDefaultAsync(),
                RoomNumber = roomNumber
            };

            db.Add(room);
            await db.SaveChangesAsync();
        }
        public async Task DeleteRoom(int roomNumber)
        {
            var room = await db.Rooms.Where(x => x.RoomNumber == roomNumber).FirstOrDefaultAsync();

            db.Remove(room);
            await db.SaveChangesAsync();
        }

        public async Task DeleteReview(int RoomTypeId)
        {
            var review = await db.Reviews.Where(x => x.RoomTypeId == RoomTypeId).ToListAsync();

            db.RemoveRange(review);
            await db.SaveChangesAsync();
        }
    }
}
