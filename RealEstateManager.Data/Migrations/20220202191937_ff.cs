using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class ff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "Houses",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "Houses",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Condition",
                table: "Houses",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Availability",
                table: "Houses",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
