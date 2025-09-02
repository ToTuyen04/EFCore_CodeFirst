using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_CodeFirst.Db.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPlayerInstrument");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInstruments_InstrumentTypeId",
                table: "PlayerInstruments",
                column: "InstrumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInstruments_PlayerId",
                table: "PlayerInstruments",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerInstruments_InstrumentTypes_InstrumentTypeId",
                table: "PlayerInstruments",
                column: "InstrumentTypeId",
                principalTable: "InstrumentTypes",
                principalColumn: "InstrumentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerInstruments_Players_PlayerId",
                table: "PlayerInstruments",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerInstruments_InstrumentTypes_InstrumentTypeId",
                table: "PlayerInstruments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerInstruments_Players_PlayerId",
                table: "PlayerInstruments");

            migrationBuilder.DropIndex(
                name: "IX_PlayerInstruments_InstrumentTypeId",
                table: "PlayerInstruments");

            migrationBuilder.DropIndex(
                name: "IX_PlayerInstruments_PlayerId",
                table: "PlayerInstruments");

            migrationBuilder.CreateTable(
                name: "PlayerPlayerInstrument",
                columns: table => new
                {
                    InstrumentsPlayerInstrumentId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPlayerInstrument", x => new { x.InstrumentsPlayerInstrumentId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerPlayerInstrument_PlayerInstruments_InstrumentsPlayerInstrumentId",
                        column: x => x.InstrumentsPlayerInstrumentId,
                        principalTable: "PlayerInstruments",
                        principalColumn: "PlayerInstrumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPlayerInstrument_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPlayerInstrument_PlayerId",
                table: "PlayerPlayerInstrument",
                column: "PlayerId");
        }
    }
}
