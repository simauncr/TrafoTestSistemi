using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafoTestSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddMuhendislerAndEngineerFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Muhendisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muhendisler", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muhendisler_AdSoyad",
                table: "Muhendisler",
                column: "AdSoyad",
                unique: true);

            migrationBuilder.AddColumn<int>(
                name: "ElektrikMuhendisiId",
                table: "TestKayitlari",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MekanikMuhendisiId",
                table: "TestKayitlari",
                type: "int",
                nullable: true);

            // Backfill engineers from existing string columns without losing data.
            migrationBuilder.Sql(@"
                INSERT INTO Muhendisler (AdSoyad)
                SELECT DISTINCT LTRIM(RTRIM(src.AdSoyad))
                FROM (
                    SELECT ElektrikMuhendisi AS AdSoyad FROM TestKayitlari
                    UNION ALL
                    SELECT MekanikMuhendisi AS AdSoyad FROM TestKayitlari
                ) AS src
                WHERE src.AdSoyad IS NOT NULL
                  AND LTRIM(RTRIM(src.AdSoyad)) <> ''
                  AND NOT EXISTS (
                      SELECT 1 FROM Muhendisler m WHERE m.AdSoyad = LTRIM(RTRIM(src.AdSoyad))
                  );

                IF NOT EXISTS (SELECT 1 FROM Muhendisler WHERE AdSoyad = N'Bilinmiyor')
                    INSERT INTO Muhendisler (AdSoyad) VALUES (N'Bilinmiyor');

                UPDATE t
                    SET ElektrikMuhendisiId = m.Id
                FROM TestKayitlari t
                INNER JOIN Muhendisler m
                    ON m.AdSoyad = CASE
                        WHEN t.ElektrikMuhendisi IS NULL OR LTRIM(RTRIM(t.ElektrikMuhendisi)) = '' THEN N'Bilinmiyor'
                        ELSE LTRIM(RTRIM(t.ElektrikMuhendisi))
                    END;

                UPDATE t
                    SET MekanikMuhendisiId = m.Id
                FROM TestKayitlari t
                INNER JOIN Muhendisler m
                    ON m.AdSoyad = CASE
                        WHEN t.MekanikMuhendisi IS NULL OR LTRIM(RTRIM(t.MekanikMuhendisi)) = '' THEN N'Bilinmiyor'
                        ELSE LTRIM(RTRIM(t.MekanikMuhendisi))
                    END;
            ");

            migrationBuilder.AlterColumn<int>(
                name: "ElektrikMuhendisiId",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MekanikMuhendisiId",
                table: "TestKayitlari",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestKayitlari_ElektrikMuhendisiId",
                table: "TestKayitlari",
                column: "ElektrikMuhendisiId");

            migrationBuilder.CreateIndex(
                name: "IX_TestKayitlari_MekanikMuhendisiId",
                table: "TestKayitlari",
                column: "MekanikMuhendisiId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestKayitlari_Muhendisler_ElektrikMuhendisiId",
                table: "TestKayitlari",
                column: "ElektrikMuhendisiId",
                principalTable: "Muhendisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestKayitlari_Muhendisler_MekanikMuhendisiId",
                table: "TestKayitlari",
                column: "MekanikMuhendisiId",
                principalTable: "Muhendisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropColumn(
                name: "ElektrikMuhendisi",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "MekanikMuhendisi",
                table: "TestKayitlari");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElektrikMuhendisi",
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

            migrationBuilder.Sql(@"
                UPDATE t
                    SET ElektrikMuhendisi = COALESCE(m.AdSoyad, N'')
                FROM TestKayitlari t
                LEFT JOIN Muhendisler m ON m.Id = t.ElektrikMuhendisiId;

                UPDATE t
                    SET MekanikMuhendisi = COALESCE(m.AdSoyad, N'')
                FROM TestKayitlari t
                LEFT JOIN Muhendisler m ON m.Id = t.MekanikMuhendisiId;
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_TestKayitlari_Muhendisler_ElektrikMuhendisiId",
                table: "TestKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_TestKayitlari_Muhendisler_MekanikMuhendisiId",
                table: "TestKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_TestKayitlari_ElektrikMuhendisiId",
                table: "TestKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_TestKayitlari_MekanikMuhendisiId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "ElektrikMuhendisiId",
                table: "TestKayitlari");

            migrationBuilder.DropColumn(
                name: "MekanikMuhendisiId",
                table: "TestKayitlari");

            migrationBuilder.DropTable(
                name: "Muhendisler");
        }
    }
}
