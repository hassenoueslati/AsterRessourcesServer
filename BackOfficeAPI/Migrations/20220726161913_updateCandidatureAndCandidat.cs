using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOfficeAPI.Migrations
{
    public partial class updateCandidatureAndCandidat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condidatures");

            migrationBuilder.AddColumn<bool>(
                name: "Statut",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Candidatures",
                columns: table => new
                {
                    CandidatFK = table.Column<int>(type: "integer", nullable: false),
                    OffreFK = table.Column<int>(type: "integer", nullable: false),
                    Etat = table.Column<int>(type: "integer", nullable: true),
                    PartinenceProfil = table.Column<int>(type: "integer", nullable: true),
                    DateCondidature = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProfileInteressant = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatures", x => new { x.OffreFK, x.CandidatFK });
                    table.ForeignKey(
                        name: "FK_Candidatures_Offres_OffreFK",
                        column: x => x.OffreFK,
                        principalTable: "Offres",
                        principalColumn: "OffreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidatures_Users_CandidatFK",
                        column: x => x.CandidatFK,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatures_CandidatFK",
                table: "Candidatures",
                column: "CandidatFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatures");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Condidatures",
                columns: table => new
                {
                    OffreFK = table.Column<int>(type: "integer", nullable: false),
                    CandidatFK = table.Column<int>(type: "integer", nullable: false),
                    DateCondidature = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Etat = table.Column<int>(type: "integer", nullable: true),
                    PartinenceProfil = table.Column<int>(type: "integer", nullable: true),
                    ProfileInteressant = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condidatures", x => new { x.OffreFK, x.CandidatFK });
                    table.ForeignKey(
                        name: "FK_Condidatures_Offres_OffreFK",
                        column: x => x.OffreFK,
                        principalTable: "Offres",
                        principalColumn: "OffreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Condidatures_Users_CandidatFK",
                        column: x => x.CandidatFK,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Condidatures_CandidatFK",
                table: "Condidatures",
                column: "CandidatFK");
        }
    }
}
