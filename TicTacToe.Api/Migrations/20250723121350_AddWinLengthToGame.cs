using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWinLengthToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinLength",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinLength",
                table: "Games");
        }
    }
}
