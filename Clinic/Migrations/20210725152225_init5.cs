using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Doctors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
