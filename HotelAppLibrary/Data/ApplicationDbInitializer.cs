using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Data
{
    public class ApplicationDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Amenity
                if(!context.Amenities.Any())
                {
                    new Amenity()
                    {
                        Id = 1,
                        Name = "Jacuzzi",
                        Icon = "fas fa-hot-tub"
                    };

                    new Amenity()
                    {
                        Id = 2,
                        Name = "Coffee",
                        Icon = "fa-solid fa-beer-mug-empty"
                    };

                    new Amenity()
                    {
                        Id = 3,
                        Name = "Laptop",
                        Icon = "bi bi-laptop"
                    };

                    new Amenity()
                    { 
                        Id = 4,
                        Name = "TV",
                        Icon = "fa-solid fa-tv"
                    };

                    context.SaveChanges();
                }
                //ApplicationUser
                if (!context.Users.Any())
                {

                    var user = new ApplicationUser()
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                        Email = "kennyolmezhotel@gmail.com",
                        UserName = "kennyolmezhotel@gmail.com",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    context.SaveChangesAsync();
                };

                //Room
                if (!context.Rooms.Any())
                {
                    new Room()
                    {
                        Id = 1,
                        RoomNumber = 1,
                        RoomTypeId = 1,
                    };

                    new Room()
                    {
                        Id = 2,
                        RoomNumber = 2,
                        RoomTypeId = 1,
                    };

                    new Room()
                    {
                        Id = 3,
                        RoomNumber = 3,
                        RoomTypeId = 2,
                    };

                    new Room()
                    {
                        Id = 4,
                        RoomNumber = 4,
                        RoomTypeId = 3,
                    };

                    new Room()
                    {
                        Id = 5,
                        RoomNumber = 5,
                        RoomTypeId = 4,
                    };

                    new Room()
                    {
                        Id = 6,
                        RoomNumber = 6,
                        RoomTypeId = 4,
                    };

                    context.SaveChanges();
                }
                //RoomType
                if (!context.RoomTypes.Any())
                {
                    new RoomType()
                    {
                        Id = 1,
                        Title = "Single Room",
                        Description = "A single room has one single bed for single occupancy. An additional bed (called an extra bed) may be added to this room at the request of a guest and charged accordingly.",
                        ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                        Amenities = context.Amenities.Where(x => x.Id == 1 && x.Id == 2).ToList()
                    };

                    new RoomType()
                    {
                        Id = 2,
                        Title = "Twin Room",
                        Description = "A twin room has two single beds for double occupancy. An extra bed may be added to this room at the request of a guest and charged accordingly. Here the bed size is normally 3 feet by 6 feet. These rooms are suitable for sharing accommodation among a group of delegates meeting.",
                        ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                        Amenities = context.Amenities.Where(x => x.Id == 1 && x.Id == 3).ToList()
                    };

                    new RoomType()
                    {
                        Id = 3,
                        Title = "Double Room",
                        Description = "A double room has one double bed for double occupancy. An extra bed may be added to this room at the request of a guest and charged accordingly. The size of the double bed is generally 4.5 feet by 6 feet.",
                        ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                        Amenities = context.Amenities.Where(x => x.Id == 2 && x.Id == 4).ToList()
                    };

                    new RoomType()
                    {
                        Id = 4,
                        Title = "Triple Room",
                        Description = "A triple room has three separate single beds and can be occupied by three guests. This type of room is suitable for groups and delegates of meetings and conferences.",
                        ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                        Amenities = context.Amenities.Where(x => x.Id == 3 && x.Id == 4).ToList()
                    };

                    context.SaveChanges();
                }



            }
        }
    }
}
