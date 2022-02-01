using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class y : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "PlotLocation",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Apartments");

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Landlords",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "Landlords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KinFirstName",
                table: "Landlords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KinLastName",
                table: "Landlords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Landlords",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Apartments",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "Apartments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LandlordId",
                table: "Apartments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Town",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "KinFirstName",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "KinLastName",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "LandlordId",
                table: "Apartments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Landlords",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId",
                table: "Landlords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PlotLocation",
                table: "Landlords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Apartments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
