using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStok.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cariler",
                columns: table => new
                {
                    carId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unvan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    adres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    vergiNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    vergiDairesi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cariler", x => x.carId);
                });

            migrationBuilder.CreateTable(
                name: "depolar",
                columns: table => new
                {
                    depoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    depoAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rafBilgisi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    konumBilgisi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depolar", x => x.depoId);
                });

            migrationBuilder.CreateTable(
                name: "kullanicilar",
                columns: table => new
                {
                    kullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    olusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullanicilar", x => x.kullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "malzemeler",
                columns: table => new
                {
                    malzemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    malzemeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kategori = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minStokMiktar = table.Column<int>(type: "int", nullable: false),
                    barkodNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    aktifPasif = table.Column<bool>(type: "bit", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_malzemeler", x => x.malzemeId);
                });

            migrationBuilder.CreateTable(
                name: "depoTransferleri",
                columns: table => new
                {
                    transferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transferNo = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    kaynakDepoId = table.Column<int>(type: "int", nullable: false),
                    hedefDepoId = table.Column<int>(type: "int", nullable: false),
                    transferTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    seriNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depoTransferleri", x => x.transferId);
                    table.ForeignKey(
                        name: "FK_depoTransferleri_depolar_hedefDepoId",
                        column: x => x.hedefDepoId,
                        principalTable: "depolar",
                        principalColumn: "depoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_depoTransferleri_depolar_kaynakDepoId",
                        column: x => x.kaynakDepoId,
                        principalTable: "depolar",
                        principalColumn: "depoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "logTakipler",
                columns: table => new
                {
                    islemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tabloAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    islemTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    islemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    detay = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    kullaniciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logTakipler", x => x.islemId);
                    table.ForeignKey(
                        name: "FK_logTakipler_kullanicilar_kullaniciId",
                        column: x => x.kullaniciId,
                        principalTable: "kullanicilar",
                        principalColumn: "kullaniciId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stoklar",
                columns: table => new
                {
                    HareketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalzemeId = table.Column<int>(type: "int", nullable: false),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    HareketTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HareketTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferansId = table.Column<int>(type: "int", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    carId = table.Column<int>(type: "int", nullable: false),
                    SeriNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stoklar", x => x.HareketId);
                    table.ForeignKey(
                        name: "FK_stoklar_cariler_carId",
                        column: x => x.carId,
                        principalTable: "cariler",
                        principalColumn: "carId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stoklar_depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "depolar",
                        principalColumn: "depoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stoklar_malzemeler_MalzemeId",
                        column: x => x.MalzemeId,
                        principalTable: "malzemeler",
                        principalColumn: "malzemeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "depoTransferDetaylari",
                columns: table => new
                {
                    detayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transferId = table.Column<int>(type: "int", nullable: false),
                    malzemeId = table.Column<int>(type: "int", nullable: false),
                    miktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depoTransferDetaylari", x => x.detayId);
                    table.ForeignKey(
                        name: "FK_depoTransferDetaylari_depoTransferleri_transferId",
                        column: x => x.transferId,
                        principalTable: "depoTransferleri",
                        principalColumn: "transferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_depoTransferDetaylari_malzemeler_malzemeId",
                        column: x => x.malzemeId,
                        principalTable: "malzemeler",
                        principalColumn: "malzemeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "irsaliyeler",
                columns: table => new
                {
                    irsaliyeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    irsaliyeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carId = table.Column<int>(type: "int", nullable: false),
                    irsaliyeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    toplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    irsaliyeTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transferId = table.Column<int>(type: "int", nullable: true),
                    durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_irsaliyeler", x => x.irsaliyeId);
                    table.ForeignKey(
                        name: "FK_irsaliyeler_cariler_carId",
                        column: x => x.carId,
                        principalTable: "cariler",
                        principalColumn: "carId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_irsaliyeler_depoTransferleri_transferId",
                        column: x => x.transferId,
                        principalTable: "depoTransferleri",
                        principalColumn: "transferId");
                });

            migrationBuilder.CreateTable(
                name: "irsaliyeDetaylari",
                columns: table => new
                {
                    detayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    irsaliyeId = table.Column<int>(type: "int", nullable: false),
                    malzemeId = table.Column<int>(type: "int", nullable: false),
                    miktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    birimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    araToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    seriNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_irsaliyeDetaylari", x => x.detayId);
                    table.ForeignKey(
                        name: "FK_irsaliyeDetaylari_irsaliyeler_irsaliyeId",
                        column: x => x.irsaliyeId,
                        principalTable: "irsaliyeler",
                        principalColumn: "irsaliyeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_irsaliyeDetaylari_malzemeler_malzemeId",
                        column: x => x.malzemeId,
                        principalTable: "malzemeler",
                        principalColumn: "malzemeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_depoTransferDetaylari_malzemeId",
                table: "depoTransferDetaylari",
                column: "malzemeId");

            migrationBuilder.CreateIndex(
                name: "IX_depoTransferDetaylari_transferId",
                table: "depoTransferDetaylari",
                column: "transferId");

            migrationBuilder.CreateIndex(
                name: "IX_depoTransferleri_hedefDepoId",
                table: "depoTransferleri",
                column: "hedefDepoId");

            migrationBuilder.CreateIndex(
                name: "IX_depoTransferleri_kaynakDepoId",
                table: "depoTransferleri",
                column: "kaynakDepoId");

            migrationBuilder.CreateIndex(
                name: "IX_irsaliyeDetaylari_irsaliyeId",
                table: "irsaliyeDetaylari",
                column: "irsaliyeId");

            migrationBuilder.CreateIndex(
                name: "IX_irsaliyeDetaylari_malzemeId",
                table: "irsaliyeDetaylari",
                column: "malzemeId");

            migrationBuilder.CreateIndex(
                name: "IX_irsaliyeler_carId",
                table: "irsaliyeler",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_irsaliyeler_transferId",
                table: "irsaliyeler",
                column: "transferId");

            migrationBuilder.CreateIndex(
                name: "IX_logTakipler_kullaniciId",
                table: "logTakipler",
                column: "kullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_stoklar_carId",
                table: "stoklar",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_stoklar_DepoId",
                table: "stoklar",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_stoklar_MalzemeId",
                table: "stoklar",
                column: "MalzemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "depoTransferDetaylari");

            migrationBuilder.DropTable(
                name: "irsaliyeDetaylari");

            migrationBuilder.DropTable(
                name: "logTakipler");

            migrationBuilder.DropTable(
                name: "stoklar");

            migrationBuilder.DropTable(
                name: "irsaliyeler");

            migrationBuilder.DropTable(
                name: "kullanicilar");

            migrationBuilder.DropTable(
                name: "malzemeler");

            migrationBuilder.DropTable(
                name: "cariler");

            migrationBuilder.DropTable(
                name: "depoTransferleri");

            migrationBuilder.DropTable(
                name: "depolar");
        }
    }
}
