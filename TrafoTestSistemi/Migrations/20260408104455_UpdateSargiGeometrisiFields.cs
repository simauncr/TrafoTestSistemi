using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSargiGeometrisiFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YG_Sapma_Radyal",
                table: "TestKayitlari",
                newName: "YG_Sapma_Radyal_U");

            migrationBuilder.RenameColumn(
                name: "AG_Sapma_Radyal",
                table: "TestKayitlari",
                newName: "YG_Sapma_Radyal_K");

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_Radyal_K",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_Radyal_U",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AG_Sapma_Radyal_K",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Sapma_Radyal_U",
                table: "TestKayitlari");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_Radyal_U",
                table: "TestKayitlari",
                newName: "YG_Sapma_Radyal");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_Radyal_K",
                table: "TestKayitlari",
                newName: "AG_Sapma_Radyal");
        }
    }
}
