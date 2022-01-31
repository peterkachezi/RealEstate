using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class new69 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentName",
                table: "Tenants");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Tenants",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId",
                table: "Landlords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Apartments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Apartments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TenantUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    AttachmentName = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUploads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantUploads");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Apartments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentName",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
