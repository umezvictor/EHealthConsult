using Microsoft.EntityFrameworkCore.Migrations;

namespace EHealthConsult.Migrations
{
    public partial class AppointmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Problem",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Complaint",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complaint",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
