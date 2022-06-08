using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingManagement.Data.Migrations
{
    public partial class AddedoneColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Rooms");
        }
    }
}
