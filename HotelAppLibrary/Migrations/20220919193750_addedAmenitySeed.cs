using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAppLibrary.Migrations
{
    public partial class addedAmenitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "fas fa-hot-tub", "Jacuzzi" },
                    { 2, "fa-solid fa-beer-mug-empty", "Coffee" },
                    { 3, "bi bi-laptop", "Laptop" },
                    { 4, "fa-solid fa-tv", "TV" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
