using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FinalDesignStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToplamAgirlik",
                table: "TestKayitlari",
                newName: "Yag_Test");

            migrationBuilder.RenameColumn(
                name: "CekirdekAgirlik",
                table: "TestKayitlari",
                newName: "Yag_Hesap");

            migrationBuilder.AddColumn<double>(
                name: "AG_DisCap_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_DisCap_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_IcCap_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_IcCap_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cekirdek_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cekirdek_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Iletken_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Iletken_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_Garanti",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Toplam_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Toplam_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_DisCap_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_DisCap_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_IcCap_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_IcCap_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AG_DisCap_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_DisCap_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_IcCap_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_IcCap_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Cekirdek_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Cekirdek_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Iletken_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Iletken_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_Garanti",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Toplam_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Toplam_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_DisCap_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_DisCap_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_IcCap_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_IcCap_Test",
                table: "TestKayitlari");

            migrationBuilder.RenameColumn(
                name: "Yag_Test",
                table: "TestKayitlari",
                newName: "ToplamAgirlik");

            migrationBuilder.RenameColumn(
                name: "Yag_Hesap",
                table: "TestKayitlari",
                newName: "CekirdekAgirlik");
        }
    }
}
