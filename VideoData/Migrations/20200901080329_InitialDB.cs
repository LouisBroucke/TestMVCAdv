using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoData.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreNaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    KlantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    Voornaam = table.Column<string>(nullable: true),
                    Straat_Nr = table.Column<string>(nullable: true),
                    Postcode = table.Column<int>(nullable: false),
                    Gemeente = table.Column<string>(nullable: true),
                    KlantStat = table.Column<string>(nullable: true),
                    HuurAantal = table.Column<int>(nullable: false),
                    DatumLid = table.Column<DateTime>(nullable: false),
                    Lidgeld = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.KlantID);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(nullable: true),
                    GenreID = table.Column<int>(nullable: false),
                    InVoorraad = table.Column<int>(nullable: false),
                    UitVoorraad = table.Column<int>(nullable: false),
                    Prijs = table.Column<decimal>(nullable: false),
                    TotaalVerhuurd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmID);
                    table.ForeignKey(
                        name: "FK_Films_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verhuringen",
                columns: table => new
                {
                    VerhuringID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantID = table.Column<int>(nullable: false),
                    FilmID = table.Column<int>(nullable: false),
                    VerhuurDatum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verhuringen", x => x.VerhuringID);
                    table.ForeignKey(
                        name: "FK_Verhuringen_Films_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Films",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verhuringen_Klanten_KlantID",
                        column: x => x.KlantID,
                        principalTable: "Klanten",
                        principalColumn: "KlantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_GenreID",
                table: "Films",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Verhuringen_FilmID",
                table: "Verhuringen",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_Verhuringen_KlantID",
                table: "Verhuringen",
                column: "KlantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verhuringen");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
