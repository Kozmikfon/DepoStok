using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStok.Migrations
{
    /// <inheritdoc />
    public partial class CarIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "carId",
                table: "stoklar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_stoklar_carId",
                table: "stoklar",
                column: "carId");

            migrationBuilder.AddForeignKey(
                name: "FK_stoklar_cariler_carId",
                table: "stoklar",
                column: "carId",
                principalTable: "cariler",
                principalColumn: "carId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stoklar_cariler_carId",
                table: "stoklar");

            migrationBuilder.DropIndex(
                name: "IX_stoklar_carId",
                table: "stoklar");

            migrationBuilder.DropColumn(
                name: "carId",
                table: "stoklar");
        }
    }
}
