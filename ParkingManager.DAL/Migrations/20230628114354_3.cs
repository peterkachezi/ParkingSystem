using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManager.DAL.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "ParkingSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "ParkingSlots");
        }
    }
}
