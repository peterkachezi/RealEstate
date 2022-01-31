using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BOMET" },
                    { 26, "MIGORI" },
                    { 27, "MOMBASA" },
                    { 28, "MURANGA" },
                    { 29, "NAIROBI" },
                    { 30, "NAKURU" },
                    { 31, "NANDI" },
                    { 32, "NAROK" },
                    { 33, "NYAMIRA" },
                    { 34, "NYANDARUA" },
                    { 25, "MERU" },
                    { 35, "NYERI" },
                    { 37, "SIAYA" },
                    { 38, "TAITA TAVETA" },
                    { 39, "TANA RIVER" },
                    { 40, "THARAKA - NITHI" },
                    { 41, "TRANS NZOIA" },
                    { 42, "URKANA" },
                    { 43, "UASIN GISHU" },
                    { 44, "VIHIGA" },
                    { 45, "WAJIR" },
                    { 36, "SAMBURU" },
                    { 46, "WEST POKOT" },
                    { 24, "MARSABIT" },
                    { 22, "MAKUENI" },
                    { 2, "BUNGOMA" },
                    { 3, "BUSIA" },
                    { 4, "ELGEYO/MARAKWET" },
                    { 5, "EMBU" },
                    { 6, "GARISSA" },
                    { 7, "HOMA BAY" },
                    { 8, "ISIOLO" },
                    { 9, "KAJIADO" },
                    { 10, "KAKAMEGA" },
                    { 23, "MANDERA" },
                    { 11, "KERICHO" },
                    { 13, "KILIFI" },
                    { 14, "KIRINYAGA" },
                    { 15, "KISII" },
                    { 16, "KISUMU" },
                    { 17, "KITUI" },
                    { 18, "KWALE" },
                    { 19, "LAIKIPIA" },
                    { 20, "LAMU" },
                    { 21, "MACHAKOS" },
                    { 12, "KIAMBU" },
                    { 47, "BARINGO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counties");
        }
    }
}
