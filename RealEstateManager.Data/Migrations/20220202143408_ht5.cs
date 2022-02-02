using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateManager.Data.Migrations
{
    public partial class ht5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Houses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Houses",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
