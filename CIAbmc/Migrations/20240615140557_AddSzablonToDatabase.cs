using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIAbmc.Migrations
{
    /// <inheritdoc />
    public partial class AddSzablonToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Szablony",
                columns: table => new
                {
                    Wersja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opracowane_dla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opracowane_przez = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Kluczowi_partnerzy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kluczowe_aktywności = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kluczowe_zasoby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Propozycja_wartości = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relacja_z_klientami = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kanaly_dotarcia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Segmenty_klientów = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Struktura_kosztow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strumienie_przychodów = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szablony", x => x.Wersja);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Szablony");
        }
    }
}
