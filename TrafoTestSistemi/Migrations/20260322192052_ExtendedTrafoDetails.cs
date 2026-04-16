using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedTrafoDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaglantiGrubu",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "CekirdekAgirlik",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CekirdekTipi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DizaynId",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DizaynTarihi",
                table: "TestKayitlari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ElektrikMuhendisi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Frekans",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "GerilimAG",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GerilimYG",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "KazanCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MekanikMuhendisi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "P0_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pk_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SacCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "SargiIsinmaAG",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SargiIsinmaYG",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestTarihi",
                table: "TestKayitlari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "ToplamAgirlik",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Garanti",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Hesap",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uk_Test",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "YagCinsi",
                table: "TestKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "YagIsinma",
                table: "TestKayitlari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaglantiGrubu",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "CekirdekAgirlik",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "CekirdekTipi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "DizaynId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "DizaynTarihi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "ElektrikMuhendisi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Frekans",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "GerilimAG",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "GerilimYG",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "KazanCinsi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "MekanikMuhendisi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "P0_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Pk_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SacCinsi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SargiIsinmaAG",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "SargiIsinmaYG",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "TestTarihi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "ToplamAgirlik",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Garanti",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Hesap",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "Uk_Test",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YagCinsi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "YagIsinma",
                table: "TestKayitlari");
        }
    }
}
