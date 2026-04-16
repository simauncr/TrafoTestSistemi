using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightSapma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AGIletken_Sapma",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cekirdek_Sapma",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YGIletken_Sapma",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Yag_Sapma",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AGIletken_Sapma",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Cekirdek_Sapma",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YGIletken_Sapma",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Yag_Sapma",
                table: "TestKayitlari");
        }
    }
}
