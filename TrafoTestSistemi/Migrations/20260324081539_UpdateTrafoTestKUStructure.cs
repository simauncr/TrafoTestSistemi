using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrafoTestKUStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YG_IcCap_Test",
                table: "TestKayitlari",
                newName: "YG_Sapma_Radyal");

            migrationBuilder.RenameColumn(
                name: "YG_IcCap_Hesap",
                table: "TestKayitlari",
                newName: "YG_Sapma_IcCap_U");

            migrationBuilder.RenameColumn(
                name: "YG_DisCap_Test",
                table: "TestKayitlari",
                newName: "YG_Sapma_IcCap_K");

            migrationBuilder.RenameColumn(
                name: "YG_DisCap_Hesap",
                table: "TestKayitlari",
                newName: "YG_Sapma_DisCap_U");

            migrationBuilder.RenameColumn(
                name: "Sapma_Radyal",
                table: "TestKayitlari",
                newName: "YG_Sapma_DisCap_K");

            migrationBuilder.RenameColumn(
                name: "Sapma_IcCap",
                table: "TestKayitlari",
                newName: "YG_IcCap_U_Test");

            migrationBuilder.RenameColumn(
                name: "Sapma_DisCap",
                table: "TestKayitlari",
                newName: "YG_IcCap_U_Hesap");

            migrationBuilder.RenameColumn(
                name: "AG_IcCap_Test",
                table: "TestKayitlari",
                newName: "YG_IcCap_K_Test");

            migrationBuilder.RenameColumn(
                name: "AG_IcCap_Hesap",
                table: "TestKayitlari",
                newName: "YG_IcCap_K_Hesap");

            migrationBuilder.RenameColumn(
                name: "AG_DisCap_Test",
                table: "TestKayitlari",
                newName: "YG_DisCap_U_Test");

            migrationBuilder.RenameColumn(
                name: "AG_DisCap_Hesap",
                table: "TestKayitlari",
                newName: "YG_DisCap_U_Hesap");

            migrationBuilder.AlterColumn<string>(
                name: "YagCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SacCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MekanikMuhendisi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KazanCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ElektrikMuhendisi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CekirdekTipi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BaglantiGrubu",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AG_DisCap_K_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_DisCap_K_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_DisCap_U_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_DisCap_U_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_IcCap_K_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_IcCap_K_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_IcCap_U_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_IcCap_U_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_DisCap_K",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_DisCap_U",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_IcCap_K",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_IcCap_U",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Sapma_Radyal",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_DisCap_K_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_DisCap_K_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AG_DisCap_K_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_DisCap_K_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_DisCap_U_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_DisCap_U_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_IcCap_K_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_IcCap_K_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_IcCap_U_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_IcCap_U_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Sapma_DisCap_K",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Sapma_DisCap_U",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Sapma_IcCap_K",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Sapma_IcCap_U",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Sapma_Radyal",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_DisCap_K_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_DisCap_K_Test",
                table: "TestKayitlari");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_Radyal",
                table: "TestKayitlari",
                newName: "YG_IcCap_Test");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_IcCap_U",
                table: "TestKayitlari",
                newName: "YG_IcCap_Hesap");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_IcCap_K",
                table: "TestKayitlari",
                newName: "YG_DisCap_Test");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_DisCap_U",
                table: "TestKayitlari",
                newName: "YG_DisCap_Hesap");

            migrationBuilder.RenameColumn(
                name: "YG_Sapma_DisCap_K",
                table: "TestKayitlari",
                newName: "Sapma_Radyal");

            migrationBuilder.RenameColumn(
                name: "YG_IcCap_U_Test",
                table: "TestKayitlari",
                newName: "Sapma_IcCap");

            migrationBuilder.RenameColumn(
                name: "YG_IcCap_U_Hesap",
                table: "TestKayitlari",
                newName: "Sapma_DisCap");

            migrationBuilder.RenameColumn(
                name: "YG_IcCap_K_Test",
                table: "TestKayitlari",
                newName: "AG_IcCap_Test");

            migrationBuilder.RenameColumn(
                name: "YG_IcCap_K_Hesap",
                table: "TestKayitlari",
                newName: "AG_IcCap_Hesap");

            migrationBuilder.RenameColumn(
                name: "YG_DisCap_U_Test",
                table: "TestKayitlari",
                newName: "AG_DisCap_Test");

            migrationBuilder.RenameColumn(
                name: "YG_DisCap_U_Hesap",
                table: "TestKayitlari",
                newName: "AG_DisCap_Hesap");

            migrationBuilder.AlterColumn<string>(
                name: "YagCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SacCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MekanikMuhendisi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "KazanCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ElektrikMuhendisi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CekirdekTipi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BaglantiGrubu",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
