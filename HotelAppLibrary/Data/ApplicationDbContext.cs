using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        
        // This is a constructor that is called when ApplicationDbContext is instantiated in (Program.cs?) / on startup
        // base() calls the constructor of the parent class which we inherit from (i.e. DbContext)
        // It injects DbContextOptions

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Amenity> Amenities { get; set; }


        // Specify/customize the DbSet properties. Modelbuilder sets the validation in the database.
        // It tells the db what the limits and constraints of the columns are.
        // The reason you do this is, maybe the validation needs to differ. For example, you may want the user to input dates now or later (fluent validation)
        // But the admin is allowed to input older dates
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bookings>()
                .Property(b => b.Email)
                .IsRequired()
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(100);

            modelBuilder.Entity<Bookings>()
                .Property(b => b.StartDate)
                .IsRequired()
                .HasColumnOrder(2)
                .HasColumnType("datetime2");

            modelBuilder.Entity<Bookings>()
                .Property(b => b.EndDate)
                .IsRequired()
                .HasColumnOrder(3)
                .HasColumnType("datetime2");

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .IsRequired();

            modelBuilder.Entity<Review>()
                .Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(2000);

            // Seeding all the necessary data. Might be a sloppy solution but gets the job done in a hurry. 

            modelBuilder.Entity<Amenity>()
                .HasData(
                new Amenity()
                {
                    Id = 1,
                    Name = "Jacuzzi",
                    Icon = "fas fa-hot-tub"
                },

                new Amenity()
                {
                    Id = 2,
                    Name = "Coffee",
                    Icon = "fa-solid fa-beer-mug-empty"
                },

                new Amenity()
                {
                    Id = 3,
                    Name = "Laptop",
                    Icon = "bi bi-laptop"
                },

                new Amenity()
                {
                    Id = 4,
                    Name = "TV",
                    Icon = "fa-solid fa-tv"
                }
                );

            modelBuilder.Entity<RoomType>()
                .HasData(
                new RoomType()
                {
                    Id = 1,
                    Title = "Single Room",
                    Description = "A single room has one single bed for single occupancy. An additional bed (called an extra bed) may be added to this room at the request of a guest and charged accordingly.",
                    ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                    Price = 50
                },

                new RoomType()
                {
                    Id = 2,
                    Title = "Twin Room",
                    Description = "A twin room has two single beds for double occupancy. An extra bed may be added to this room at the request of a guest and charged accordingly. Here the bed size is normally 3 feet by 6 feet. These rooms are suitable for sharing accommodation among a group of delegates meeting.",
                    ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                    Price = 75
                },

                new RoomType()
                {
                    Id = 3,
                    Title = "Double Room",
                    Description = "A double room has one double bed for double occupancy. An extra bed may be added to this room at the request of a guest and charged accordingly. The size of the double bed is generally 4.5 feet by 6 feet.",
                    ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                    Price = 105
                },

                new RoomType()
                {
                    Id = 4,
                    Title = "Triple Room",
                    Description = "A triple room has three separate single beds and can be occupied by three guests. This type of room is suitable for groups and delegates of meetings and conferences.",
                    ImgUrl = "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy",
                    Price = 160
                }
                );


            // Seeding the navigation property with an anonymous type, populates the junction table with UsingEntity

            modelBuilder.Entity<RoomType>()
                .HasMany(p => p.Amenities)
                .WithMany(p => p.RoomTypes)
                .UsingEntity(j => j.HasData(
                    new { AmenitiesId = 1, RoomTypesId = 1 },
                    new { AmenitiesId = 2, RoomTypesId = 1 },
                    new { AmenitiesId = 3, RoomTypesId = 2 },
                    new { AmenitiesId = 4, RoomTypesId = 2 }
                ));


            modelBuilder.Entity<Room>()
                .HasData(
                new Room()
                {
                    Id = 1,
                    RoomNumber = 1,
                    RoomTypeId = 1,
                },

                new Room()
                {
                    Id = 2,
                    RoomNumber = 2,
                    RoomTypeId = 1,
                },

                new Room()
                {
                    Id = 3,
                    RoomNumber = 3,
                    RoomTypeId = 2,
                },

                new Room()
                {
                    Id = 4,
                    RoomNumber = 4,
                    RoomTypeId = 3,
                },

                new Room()
                {
                    Id = 5,
                    RoomNumber = 5,
                    RoomTypeId = 4,
                },

                new Room()
                {
                    Id = 6,
                    RoomNumber = 6,
                    RoomTypeId = 4,
                }
                );


            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";

            const string ROLE_ID = ADMIN_ID;
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Admin",
                NormalizedName = "Admin"
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "kennyolmezhotel@gmail.com",
                NormalizedUserName = "kennyolmezhotel@gmail.com",
                Email = "kennyolmezhotel@gmail.com",
                NormalizedEmail = "kennyolmezhotel@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "password"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });



            //    // Admin role
            //    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            //    {
            //        Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //        Name = "TestAdmin",
            //        NormalizedName = "TestAdmin".ToUpper()
            //    });

            //    var hasher = new PasswordHasher<IdentityUser>();

            //    modelBuilder.Entity<IdentityUser>().HasData(
            //        new IdentityUser
            //        {
            //            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
            //            Email = "kennyolmezhotel@gmail.com",
            //            EmailConfirmed = true,
            //            UserName = "owner",
            //            NormalizedUserName = "Owner",
            //            PasswordHash = hasher.HashPassword(null, "secret")
            //        }
            //        );

            //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            //    }
            //);
        }

    }
}
