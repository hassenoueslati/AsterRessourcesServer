using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackOfficeAPI.Migrations
{
    public partial class AddQuestionsToOffre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Libelle = table.Column<string>(type: "text", nullable: true),
                    Suggestion = table.Column<List<string>>(type: "text[]", nullable: true),
                    EstObligatoire = table.Column<bool>(type: "boolean", nullable: true),
                    OffreId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Offres_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offres",
                        principalColumn: "OffreId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_OffreId",
                table: "Question",
                column: "OffreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
