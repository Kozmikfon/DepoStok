using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStok.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_logTakipler_kullanicilar_kullaniciId",
                table: "logTakipler");

            migrationBuilder.DropTable(
                name: "kullanicilar");

            migrationBuilder.AlterColumn<string>(
                name: "kullaniciId",
                table: "logTakipler",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_logTakipler_AspNetUsers_kullaniciId",
                table: "logTakipler",
                column: "kullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_logTakipler_AspNetUsers_kullaniciId",
                table: "logTakipler");

            migrationBuilder.AlterColumn<int>(
                name: "kullaniciId",
                table: "logTakipler",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "kullanicilar",
                columns: table => new
                {
                    kullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    olusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullanicilar", x => x.kullaniciId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_logTakipler_kullanicilar_kullaniciId",
                table: "logTakipler",
                column: "kullaniciId",
                principalTable: "kullanicilar",
                principalColumn: "kullaniciId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
