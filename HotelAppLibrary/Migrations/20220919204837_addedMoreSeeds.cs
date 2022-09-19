using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAppLibrary.Migrations
{
    public partial class addedMoreSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Description", "ImgUrl", "Price", "Size", "Title" },
                values: new object[,]
                {
                    { 1, "A single room has one single bed for single occupancy. An additional bed (called an extra bed) may be added to this room at the request of a guest and charged accordingly.", "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy", 0m, 0, "Single Room" },
                    { 2, "A twin room has two single beds for double occupancy. An extra bed may be added to this room at the request of a guest and charged accordingly. Here the bed size is normally 3 feet by 6 feet. These rooms are suitable for sharing accommodation among a group of delegates meeting.", "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy", 0m, 0, "Twin Room" },
                    { 3, "A double room has one double bed for double occupancy. An extra bed may be added to this room at the request of a guest and charged accordingly. The size of the double bed is generally 4.5 feet by 6 feet.", "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy", 0m, 0, "Double Room" },
                    { 4, "A triple room has three separate single beds and can be occupied by three guests. This type of room is suitable for groups and delegates of meetings and conferences.", "https://webbox.imgix.net/images/owvecfmxulwbfvxm/c56a0c0d-8454-431a-9b3e-f420c72e82e3.jpg?auto=format,compress&fit=crop&crop=entropy", 0m, 0, "Triple Room" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 3 },
                    { 5, 5, 4 },
                    { 6, 6, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
