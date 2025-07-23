using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitBoardAndMoves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    CurrentPlayer = table.Column<int>(type: "integer", nullable: false),
                    Winner = table.Column<int>(type: "integer", nullable: true),
                    IsDraw = table.Column<bool>(type: "boolean", nullable: false),
                    Board = table.Column<string>(type: "text", nullable: false),
                    WinLength = table.Column<int>(type: "integer", nullable: false, defaultValue: 3)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: false),
                    Col = table.Column<int>(type: "integer", nullable: false),
                    Player = table.Column<int>(type: "integer", nullable: false),
                    ETag = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GameId",
                table: "Moves",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
