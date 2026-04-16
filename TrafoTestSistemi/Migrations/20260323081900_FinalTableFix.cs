using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FinalTableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YagIsinma",
                table: "TestKayitlari",
                newName: "YG_SargiIsinma_Test");

            migrationBuilder.RenameColumn(
                name: "Toplam_Test",
                table: "TestKayitlari",
                newName: "YG_SargiIsinma_Sapma_HT");

            migrationBuilder.RenameColumn(
                name: "Toplam_Hesap",
                table: "TestKayitlari",
                newName: "YG_SargiIsinma_Sapma_GT");

            migrationBuilder.RenameColumn(
                name: "SargiIsinmaYG",
                table: "TestKayitlari",
                newName: "YG_SargiIsinma_Sapma_GH");

            migrationBuilder.RenameColumn(
                name: "SargiIsinmaAG",
                table: "TestKayitlari",
                newName: "YG_SargiIsinma_Hesap");

            migrationBuilder.RenameColumn(
                name: "P55_Garanti",
                table: "TestKayitlari",
                newName: "YG_SargiIsinma_Garanti");

            migrationBuilder.RenameColumn(
                name: "Iletken_Test",
                table: "TestKayitlari",
                newName: "YG_Grad_Test");

            migrationBuilder.RenameColumn(
                name: "Iletken_Hesap",
                table: "TestKayitlari",
                newName: "YG_Grad_Sapma_HT");

            migrationBuilder.AlterColumn<string>(
                name: "NameSurname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "DizaynId",
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

            migrationBuilder.AddColumn<double>(
                name: "AGIletken_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AGIletken_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Grad_Garanti",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Grad_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Grad_Sapma_GH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Grad_Sapma_GT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Grad_Sapma_HT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_Grad_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_SargiIsinma_Garanti",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_SargiIsinma_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_SargiIsinma_Sapma_GH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_SargiIsinma_Sapma_GT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_SargiIsinma_Sapma_HT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AG_SargiIsinma_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CekirdekAgirlik",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P0_Sapma_GH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P0_Sapma_GT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P0_Sapma_HT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P0_Tolerans",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_ElekGaran",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_MekHesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_Sapma_EGH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_Sapma_MGT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P55_Sapma_MHT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pk_Sapma_GH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pk_Sapma_GT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pk_Sapma_HT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pk_Tolerans",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sapma_DisCap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sapma_IcCap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sapma_Radyal",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Sapma_GH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Sapma_GT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Sapma_HT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Tolerans",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YGIletken_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YGIletken_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_Grad_Garanti",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_Grad_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_Grad_Sapma_GH",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YG_Grad_Sapma_GT",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AGIletken_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AGIletken_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Grad_Garanti",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Grad_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Grad_Sapma_GH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Grad_Sapma_GT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Grad_Sapma_HT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_Grad_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_SargiIsinma_Garanti",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_SargiIsinma_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_SargiIsinma_Sapma_GH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_SargiIsinma_Sapma_GT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_SargiIsinma_Sapma_HT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "AG_SargiIsinma_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "CekirdekAgirlik",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P0_Sapma_GH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P0_Sapma_GT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P0_Sapma_HT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P0_Tolerans",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_ElekGaran",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_MekHesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_Sapma_EGH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_Sapma_MGT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P55_Sapma_MHT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Pk_Sapma_GH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Pk_Sapma_GT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Pk_Sapma_HT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Pk_Tolerans",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Sapma_DisCap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Sapma_IcCap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Sapma_Radyal",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Sapma_GH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Sapma_GT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Sapma_HT",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Tolerans",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YGIletken_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YGIletken_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_Grad_Garanti",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_Grad_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_Grad_Sapma_GH",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YG_Grad_Sapma_GT",
                table: "TestKayitlari");

            migrationBuilder.RenameColumn(
                name: "YG_SargiIsinma_Test",
                table: "TestKayitlari",
                newName: "YagIsinma");

            migrationBuilder.RenameColumn(
                name: "YG_SargiIsinma_Sapma_HT",
                table: "TestKayitlari",
                newName: "Toplam_Test");

            migrationBuilder.RenameColumn(
                name: "YG_SargiIsinma_Sapma_GT",
                table: "TestKayitlari",
                newName: "Toplam_Hesap");

            migrationBuilder.RenameColumn(
                name: "YG_SargiIsinma_Sapma_GH",
                table: "TestKayitlari",
                newName: "SargiIsinmaYG");

            migrationBuilder.RenameColumn(
                name: "YG_SargiIsinma_Hesap",
                table: "TestKayitlari",
                newName: "SargiIsinmaAG");

            migrationBuilder.RenameColumn(
                name: "YG_SargiIsinma_Garanti",
                table: "TestKayitlari",
                newName: "P55_Garanti");

            migrationBuilder.RenameColumn(
                name: "YG_Grad_Test",
                table: "TestKayitlari",
                newName: "Iletken_Test");

            migrationBuilder.RenameColumn(
                name: "YG_Grad_Sapma_HT",
                table: "TestKayitlari",
                newName: "Iletken_Hesap");

            migrationBuilder.AlterColumn<string>(
                name: "NameSurname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                name: "DizaynId",
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
        }
    }
}
