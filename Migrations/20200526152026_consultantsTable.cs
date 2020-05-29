using Microsoft.EntityFrameworkCore.Migrations;

namespace EHealthConsult.Migrations
{
    public partial class consultantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "Consultants");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Consultants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Consultants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkypeId",
                table: "Consultants",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Consultants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ZoomId",
                table: "Consultants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "SkypeId",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "ZoomId",
                table: "Consultants");

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "Consultants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
