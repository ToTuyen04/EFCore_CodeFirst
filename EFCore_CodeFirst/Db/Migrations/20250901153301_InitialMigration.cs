using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore_CodeFirst.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstrumentTypes",
                columns: table => new
                {
                    InstrumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTypes", x => x.InstrumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerInstruments",
                columns: table => new
                {
                    PlayerInstrumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    InstrumentTypeId = table.Column<int>(type: "int", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInstruments", x => x.PlayerInstrumentId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

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

            migrationBuilder.InsertData(
                table: "InstrumentTypes",
                columns: new[] { "InstrumentTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Acoustic Guitar" },
                    { 2, "Electric Guitar" },
                    { 3, "Classic Guitar" },
                    { 4, "Drums" },
                    { 5, "Bass" },
                    { 6, "Keyboard" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPlayerInstrument_PlayerId",
                table: "PlayerPlayerInstrument",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentTypes");

            migrationBuilder.DropTable(
                name: "PlayerPlayerInstrument");

            migrationBuilder.DropTable(
                name: "PlayerInstruments");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
