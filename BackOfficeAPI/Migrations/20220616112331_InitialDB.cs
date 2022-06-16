using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackOfficeAPI.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<int>(type: "integer", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Proffesions = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Offres",
                columns: table => new
                {
                    OffreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    Etat = table.Column<int>(type: "integer", nullable: false),
                    Proffesions = table.Column<int[]>(type: "integer[]", nullable: false),
                    AdminFK = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offres", x => x.OffreId);
                    table.ForeignKey(
                        name: "FK_Offres_Users_AdminFK",
                        column: x => x.AdminFK,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Condidatures",
                columns: table => new
                {
                    CandidatFK = table.Column<int>(type: "integer", nullable: false),
                    OffreFK = table.Column<int>(type: "integer", nullable: false),
                    Etat = table.Column<int>(type: "integer", nullable: false),
                    PartinenceProfil = table.Column<int>(type: "integer", nullable: false),
                    DateCondidature = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Offres_AdminFK",
                table: "Offres",
                column: "AdminFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condidatures");

            migrationBuilder.DropTable(
                name: "Offres");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
