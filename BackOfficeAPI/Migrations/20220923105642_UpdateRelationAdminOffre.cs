using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOfficeAPI.Migrations
{
    public partial class UpdateRelationAdminOffre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offres_Users_AdminFK",
                table: "Offres");

            migrationBuilder.AddForeignKey(
                name: "FK_Offres_Users_AdminFK",
                table: "Offres",
                column: "AdminFK",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offres_Users_AdminFK",
                table: "Offres");

            migrationBuilder.AddForeignKey(
                name: "FK_Offres_Users_AdminFK",
                table: "Offres",
                column: "AdminFK",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
