using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStok.Migrations
{
    /// <inheritdoc />
    public partial class Transfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_irsaliyeler_depoTransferleri_transferId",
                table: "irsaliyeler");

            migrationBuilder.DropIndex(
                name: "IX_irsaliyeler_transferId",
                table: "irsaliyeler");

            migrationBuilder.DropColumn(
                name: "transferId",
                table: "irsaliyeler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "transferId",
                table: "irsaliyeler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_irsaliyeler_transferId",
                table: "irsaliyeler",
                column: "transferId");

            migrationBuilder.AddForeignKey(
                name: "FK_irsaliyeler_depoTransferleri_transferId",
                table: "irsaliyeler",
                column: "transferId",
                principalTable: "depoTransferleri",
                principalColumn: "transferId");
        }
    }
}
