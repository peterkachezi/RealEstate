using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class county : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KinName",
                table: "Tenants");

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "Tenants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KinFirstName",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KinLastName",
                table: "Tenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "KinFirstName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "KinLastName",
                table: "Tenants");

            migrationBuilder.AddColumn<string>(
                name: "KinName",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
