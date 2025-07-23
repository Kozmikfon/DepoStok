using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStok.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_depoTransferDetaylari_depoTransferleri_malzemeId",
                table: "depoTransferDetaylari");

            migrationBuilder.AlterColumn<string>(
                name: "kategori",
                table: "malzemeler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birim",
                table: "malzemeler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "kullanicilar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_depoTransferDetaylari_transferId",
                table: "depoTransferDetaylari",
                column: "transferId");

            migrationBuilder.AddForeignKey(
                name: "FK_depoTransferDetaylari_depoTransferleri_transferId",
                table: "depoTransferDetaylari",
                column: "transferId",
                principalTable: "depoTransferleri",
                principalColumn: "transferId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_depoTransferDetaylari_depoTransferleri_transferId",
                table: "depoTransferDetaylari");

            migrationBuilder.DropIndex(
                name: "IX_depoTransferDetaylari_transferId",
                table: "depoTransferDetaylari");

            migrationBuilder.AlterColumn<string>(
                name: "kategori",
                table: "malzemeler",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "birim",
                table: "malzemeler",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "kullanicilar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_depoTransferDetaylari_depoTransferleri_malzemeId",
                table: "depoTransferDetaylari",
                column: "malzemeId",
                principalTable: "depoTransferleri",
                principalColumn: "transferId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
