using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOfficeAPI.Migrations
{
    public partial class AddDateOuvertureToOffre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOuverture",
                table: "Offres",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOuverture",
                table: "Offres");
        }
    }
}
