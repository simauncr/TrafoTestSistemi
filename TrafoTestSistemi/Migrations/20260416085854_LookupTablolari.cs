using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class LookupTablolari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CekirdekTipi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "KazanCinsi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SacCinsi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YagCinsi",
                table: "TestKayitlari");

            migrationBuilder.AddColumn<int>(
                name: "CekirdekTipiId",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "KazanCinsiId",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "SacCinsiId",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "YagCinsiId",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "CekirdekTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CekirdekTipleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KazanCinsleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KazanCinsleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SacCinsleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SacCinsleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YagCinsleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YagCinsleri", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CekirdekTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Yuvarlak" },
                    { 2, "Oval" }
                });

            migrationBuilder.InsertData(
                table: "KazanCinsleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Dalga Duvar" },
                    { 2, "Düz Duvar" }
                });

            migrationBuilder.InsertData(
                table: "SacCinsleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "M070-23P" },
                    { 2, "M075-23P" },
                    { 3, "M080-23P" },
                    { 4, "M085-23P" },
                    { 5, "M130" }
                });

            migrationBuilder.InsertData(
                table: "YagCinsleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Mineral" },
                    { 2, "Midel" },
                    { 3, "FR3" },
                    { 4, "Silikon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestKayitlari_CekirdekTipiId",
                table: "TestKayitlari",
                column: "CekirdekTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_TestKayitlari_KazanCinsiId",
                table: "TestKayitlari",
                column: "KazanCinsiId");

            migrationBuilder.CreateIndex(
                name: "IX_TestKayitlari_SacCinsiId",
                table: "TestKayitlari",
                column: "SacCinsiId");

            migrationBuilder.CreateIndex(
                name: "IX_TestKayitlari_YagCinsiId",
                table: "TestKayitlari",
                column: "YagCinsiId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestKayitlari_CekirdekTipleri_CekirdekTipiId",
                table: "TestKayitlari",
                column: "CekirdekTipiId",
                principalTable: "CekirdekTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestKayitlari_KazanCinsleri_KazanCinsiId",
                table: "TestKayitlari",
                column: "KazanCinsiId",
                principalTable: "KazanCinsleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestKayitlari_SacCinsleri_SacCinsiId",
                table: "TestKayitlari",
                column: "SacCinsiId",
                principalTable: "SacCinsleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestKayitlari_YagCinsleri_YagCinsiId",
                table: "TestKayitlari",
                column: "YagCinsiId",
                principalTable: "YagCinsleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestKayitlari_CekirdekTipleri_CekirdekTipiId",
                table: "TestKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_TestKayitlari_KazanCinsleri_KazanCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_TestKayitlari_SacCinsleri_SacCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_TestKayitlari_YagCinsleri_YagCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropTable(
                name: "CekirdekTipleri");

            migrationBuilder.DropTable(
                name: "KazanCinsleri");

            migrationBuilder.DropTable(
                name: "SacCinsleri");

            migrationBuilder.DropTable(
                name: "YagCinsleri");

            migrationBuilder.DropIndex(
                name: "IX_TestKayitlari_CekirdekTipiId",
                table: "TestKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_TestKayitlari_KazanCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_TestKayitlari_SacCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_TestKayitlari_YagCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "CekirdekTipiId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "KazanCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SacCinsiId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YagCinsiId",
                table: "TestKayitlari");

            migrationBuilder.AddColumn<string>(
                name: "CekirdekTipi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KazanCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SacCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "YagCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
