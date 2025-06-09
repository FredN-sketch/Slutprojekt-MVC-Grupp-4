using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Slutprojekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedType = table.Column<int>(type: "int", nullable: false),
                    BreedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreedTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BreedTypes",
                columns: new[] { "Id", "BreedTypeName" },
                values: new object[,]
                {
                    { 1, "Grupp 1 - Vall-, boskaps- och herdehundar" },
                    { 2, "Grupp 2 - Schnauzer och pinscher, molosser och bergshundar samt sennenhundar" },
                    { 3, "Grupp 3 - Terrier" },
                    { 4, "Grupp 4 - Taxar" },
                    { 5, "Grupp 5 - Spetsar och raser av urhundstyp" },
                    { 6, "Grupp 6 - Drivande hundar samt sök- och spårhundar" },
                    { 7, "Grupp 7 - Stående fågelhundar" },
                    { 8, "Grupp 8 - Stötande hundar, apporterande hundar och vattenhundar" },
                    { 9, "Grupp 9 - Sällskapshundar" },
                    { 10, "Grupp 10 - Vinthundar" }
                });

            migrationBuilder.InsertData(
                table: "Breeds",
                columns: new[] { "Id", "BreedName", "BreedType", "Description" },
                values: new object[,]
                {
                    { 5, "Tysk spets/mittelspitz", 5, "Livlig, lättlärd pälsboll som hänger med" },
                    { 8, "Tysk schäferhund", 1, "Samarbetsvillig, livlig och uppmärksam jobbkompis" },
                    { 9, "Vit herdehud", 1, "Livlig, lättlärd sällskapshund med behov av aktivitet" },
                    { 10, "Collie, långhårig", 1, "Massa mjuk päls, elegant, lättlärd och gillar aktiviteter" },
                    { 11, "Chihuahua, korthårig", 9, "Liten och sällskaplig hund som kan ta ton (vill inte bli uppäten!)" },
                    { 12, "Greyhound", 10, "Vänlig, envis, stor och specialist på kapplöpning." },
                    { 14, "Afghanhund", 10, "Självständig skönhet med böljande päls." },
                    { 23, "Boxer", 2, "Alert, arbetsvillig och livsglad bästa vän" },
                    { 24, "Grand danois", 2, "Trofast, stor, stark och pampig" },
                    { 25, "Leonberger", 2, "Behaglig och följsam med behov av fast hand" },
                    { 32, "Labrador retriever", 8, "Social och stark apportör som är duktig på det mesta" },
                    { 33, "Golden retriever", 8, "Vänlig och aktiv med stor passion för vatten" },
                    { 36, "Bedlingtonterrier", 3, "Annorlunda utseende, charmig med stark vilja." },
                    { 47, "Tax", 4, "Vänlig, envis och uthållig trots sina korta ben" },
                    { 66, "Basset hound", 6, "Social, tillgiven kortbent spårexpert" },
                    { 67, "Beagle", 6, "Envis, arbetsvillig och glad" },
                    { 71, "Engelsk setter", 7, "Energisk och krävande med passion för fågeljakt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "BreedTypes");
        }
    }
}
