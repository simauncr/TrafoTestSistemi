using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TrafoTestSistemi.Models;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(TrafoContext))]
    [Migration("20260514090000_AddSapmaMetrics")]
    public partial class AddSapmaMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SapmaGH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0d);

            migrationBuilder.AddColumn<double>(
                name: "SapmaGT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0d);

            migrationBuilder.AddColumn<double>(
                name: "SapmaHT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0d);

            migrationBuilder.Sql(@"
                UPDATE TestKayitlari
                SET
                    SapmaGH = ROUND((P0_Sapma_GH + Pk_Sapma_GH + Uk_Sapma_GH) / 3.0, 2),
                    SapmaGT = ROUND((P0_Sapma_GT + Pk_Sapma_GT + Uk_Sapma_GT) / 3.0, 2),
                    SapmaHT = ROUND((P0_Sapma_HT + Pk_Sapma_HT + Uk_Sapma_HT) / 3.0, 2);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SapmaGH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SapmaGT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SapmaHT",
                table: "TestKayitlari");
        }
    }
}