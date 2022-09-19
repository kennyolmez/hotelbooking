using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAppLibrary.Migrations
{
    public partial class addmigrationfixedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "6e1b9ac1-281e-4748-b5ff-1074ff228d8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "1de98154-af1d-488e-ab0b-2cc695bf4c14", "kennyolmezhotel@gmail.com", "AQAAAAEAACcQAAAAEBdarxsfLljqP/k0+PIL5mCYIRBNDDypIPvXJcRL8g3H4eNsyGp6zOlogiBjy+bBJw==", "kennyolmezhotel@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "3c214bf3-5858-4d6b-bd46-0ba8dfabbf5c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "1cbe430f-2992-41f9-a7e8-690fe8b7dcf7", "Admin", "AQAAAAEAACcQAAAAEBnCrDICOgSxgJeEsRDH2HTFG0toOH7F6k1t1J+LIT13Z3EFz3wUyTf7H2+RZY/+WQ==", "Admin" });
        }
    }
}
