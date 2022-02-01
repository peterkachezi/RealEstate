using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class y1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Landlords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNumber",
                table: "Landlords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "Landlords");
        }
    }
}
