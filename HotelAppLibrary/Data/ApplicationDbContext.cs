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


            // Admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                Name = "Admin", NormalizedName = "Admin".ToUpper() });

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    Email = "kennyolmezhotel@gmail.com",
                    EmailConfirmed = true,
                    UserName = "owner",
                    NormalizedUserName = "Owner",
                    PasswordHash = hasher.HashPassword(null, "secret")
                }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );
        }

    }
}
