using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EHealthConsult.Migrations
{
    public partial class UpdatedSomeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
