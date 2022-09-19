using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAppLibrary.Migrations
{
    public partial class addedAmenities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityRoomType_Amenity_AmenitiesId",
                table: "AmenityRoomType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amenity",
                table: "Amenity");

            migrationBuilder.RenameTable(
                name: "Amenity",
                newName: "Amenities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amenities",
                table: "Amenities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityRoomType_Amenities_AmenitiesId",
                table: "AmenityRoomType",
                column: "AmenitiesId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityRoomType_Amenities_AmenitiesId",
                table: "AmenityRoomType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amenities",
                table: "Amenities");

            migrationBuilder.RenameTable(
                name: "Amenities",
                newName: "Amenity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amenity",
                table: "Amenity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityRoomType_Amenity_AmenitiesId",
                table: "AmenityRoomType",
                column: "AmenitiesId",
                principalTable: "Amenity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
