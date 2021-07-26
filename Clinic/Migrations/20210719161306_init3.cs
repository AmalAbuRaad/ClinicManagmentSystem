using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientNamee",
                table: "Patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientNamee",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
