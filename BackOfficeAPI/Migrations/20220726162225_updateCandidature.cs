using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOfficeAPI.Migrations
{
    public partial class updateCandidature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCondidature",
                table: "Candidatures",
                newName: "DateCandidature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCandidature",
                table: "Candidatures",
                newName: "DateCondidature");
        }
    }
}
