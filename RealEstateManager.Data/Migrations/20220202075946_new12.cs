using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class new12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estate",
                table: "Apartments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalAddress",
                table: "Apartments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estate",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "PhysicalAddress",
                table: "Apartments");
        }
    }
}
