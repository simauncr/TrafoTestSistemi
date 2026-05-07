using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrafoTestSistemi.Models;
using ClosedXML.Excel;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrafoTestSistemi.Controllers
{
    public class TrafoTestsController : Controller
    {
        private readonly TrafoContext _context;

        public TrafoTestsController(TrafoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? q)
        {
            var aramaMetni = q?.Trim();
            if (!string.IsNullOrWhiteSpace(aramaMetni))
            {
                var eslesmeler = await FiltrelenmisTrafoSorgusu(aramaMetni)
                    .Select(x => new { x.Id })
                    .ToListAsync();

                if (eslesmeler.Count == 1)
                {
                    return RedirectToAction(nameof(Edit), new { id = eslesmeler[0].Id });
                }
            }

            ViewBag.SearchQuery = aramaMetni;
            var veriler = await _context.TestKayitlari.ToListAsync();
            return View(veriler);
        }

        public async Task<IActionResult> Analiz()
        {
            var testler = await _context.TestKayitlari
                .Include(x => x.YagCinsi)
                .Include(x => x.SacCinsi)
                .Include(x => x.CekirdekTipi)
                .Include(x => x.KazanCinsi)
                .Include(x => x.ElektrikMuhendisi)
                .Include(x => x.MekanikMuhendisi)
                .ToListAsync();

            ViewBag.Toplam = testler.Count;
            ViewBag.Uygun = testler.Count(x => x.Sonuc != null && x.Sonuc.ToUpper().Trim() == "UYGUN");
            ViewBag.UygunDegil = testler.Count(x => x.Sonuc != null && (x.Sonuc.ToUpper().Trim().Contains("DEĞİL") || x.Sonuc.ToUpper().Trim() == "HATALI"));

            string GucKategorisiOlustur(double guc)
            {
                if (guc < 500) return "0-500 kVA";
                if (guc < 1000) return "500-1000 kVA";
                if (guc < 2000) return "1000-2000 kVA";
                if (guc < 5000) return "2000-5000 kVA";
                return "5000+ kVA";
            }

            ViewBag.AnalysisRecords = testler
                .Select(x => new
                {
                    x.Id,
                    Musteri = string.IsNullOrWhiteSpace(x.Musteri) ? "Tanimsiz" : x.Musteri,
                    ProjeAdi = string.IsNullOrWhiteSpace(x.ProjeAdi) ? "Tanimsiz" : x.ProjeAdi,
                    TasarimNo = string.IsNullOrWhiteSpace(x.TasarimNo) ? "Tanimsiz" : x.TasarimNo,
                    DizaynId = string.IsNullOrWhiteSpace(x.DizaynId) ? "Tanimsiz" : x.DizaynId,
                    MusteriProje = $"{(string.IsNullOrWhiteSpace(x.Musteri) ? "Tanimsiz" : x.Musteri)} / {(string.IsNullOrWhiteSpace(x.ProjeAdi) ? "Tanimsiz" : x.ProjeAdi)}",
                    DizaynTarihi = x.DizaynTarihi.ToString("yyyy-MM-dd"),
                    TestTarihi = x.TestTarihi.ToString("yyyy-MM-dd"),
                    ElektrikMuhendisi = string.IsNullOrWhiteSpace(x.ElektrikMuhendisi!.AdSoyad) ? "Tanimsiz" : x.ElektrikMuhendisi.AdSoyad,
                    MekanikMuhendisi = x.MekanikMuhendisi == null || string.IsNullOrWhiteSpace(x.MekanikMuhendisi.AdSoyad) ? "Tanimsiz" : x.MekanikMuhendisi.AdSoyad,
                    Muhendis = string.IsNullOrWhiteSpace(x.ElektrikMuhendisi!.AdSoyad) ? "Tanimsiz" : x.ElektrikMuhendisi.AdSoyad,
                    x.Guc,
                    x.GerilimYG,
                    x.GerilimAG,
                    x.Frekans,
                    GucKategorisi = GucKategorisiOlustur(x.Guc),
                    BaglantiGrubu = string.IsNullOrWhiteSpace(x.BaglantiGrubu) ? "Tanimsiz" : x.BaglantiGrubu,
                    CekirdekTipi = x.CekirdekTipi == null || string.IsNullOrWhiteSpace(x.CekirdekTipi.Ad) ? "Tanimsiz" : x.CekirdekTipi.Ad,
                    KazanCinsi = x.KazanCinsi == null || string.IsNullOrWhiteSpace(x.KazanCinsi.Ad) ? "Tanimsiz" : x.KazanCinsi.Ad,
                    YagCinsi = x.YagCinsi == null || string.IsNullOrWhiteSpace(x.YagCinsi.Ad) ? "Tanimsiz" : x.YagCinsi.Ad,
                    SacCinsi = x.SacCinsi == null || string.IsNullOrWhiteSpace(x.SacCinsi.Ad) ? "Tanimsiz" : x.SacCinsi.Ad,
                    P0Sapma = Math.Round(Math.Abs(x.P0_Sapma_HT), 2),
                    PkSapma = Math.Round(Math.Abs(x.Pk_Sapma_HT), 2),
                    UkSapma = Math.Round(Math.Abs(x.Uk_Sapma_HT), 2),
                    Sonuc = string.IsNullOrWhiteSpace(x.Sonuc) ? "BEKLEMEDE" : x.Sonuc
                })
                .ToList();

            if (ViewBag.Toplam > 0 && (int)ViewBag.Uygun == 0 && (int)ViewBag.UygunDegil == 0)
            {
                ViewBag.UygunDegil = ViewBag.Toplam;
            }
            return View();
        }

        public async Task<IActionResult> ExcelIndir()
        {
            var veriler = await _context.TestKayitlari
                .Include(x => x.ElektrikMuhendisi)
                .Include(x => x.MekanikMuhendisi)
                .ToListAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Trafo Detayli Rapor");
                string[] headers = {
                    "Proje Adı", "Tasarım No", "Müşteri", "Dizayn Id", "Elek. Müh.", "Mek. Müh.",
                    "Güç (kVA)", "YG Gerilim", "AG Gerilim", "Bağlantı Grubu", "Frekans",
                    "AG İç Çap K (H)", "AG İç Çap U (H)", "YG İç Çap K (H)", "YG İç Çap U (H)",
                    "P0 Garanti", "P0 Test", "P0 HT Sapma (%)",
                    "Pk Garanti", "Pk Test", "Pk HT Sapma (%)",
                    "UK Garanti", "UK Test", "UK HT Sapma (%)",
                    "Durum"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(1, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                int row = 2;
                foreach (var item in veriler)
                {
                    worksheet.Cell(row, 1).SetValue(item.ProjeAdi ?? "");
                    worksheet.Cell(row, 2).SetValue(item.TasarimNo ?? "");
                    worksheet.Cell(row, 3).SetValue(item.Musteri ?? "");
                    worksheet.Cell(row, 4).SetValue(item.DizaynId ?? "");
                    worksheet.Cell(row, 5).SetValue(item.ElektrikMuhendisi?.AdSoyad ?? "");
                    worksheet.Cell(row, 6).SetValue(item.MekanikMuhendisi?.AdSoyad ?? "");
                    worksheet.Cell(row, 7).SetValue(item.Guc);
                    worksheet.Cell(row, 8).SetValue(item.GerilimYG);
                    worksheet.Cell(row, 9).SetValue(item.GerilimAG);
                    worksheet.Cell(row, 10).SetValue(item.BaglantiGrubu ?? "");
                    worksheet.Cell(row, 11).SetValue(item.Frekans);
                    worksheet.Cell(row, 12).SetValue(item.AG_IcCap_K_Hesap);
                    worksheet.Cell(row, 13).SetValue(item.AG_IcCap_U_Hesap);
                    worksheet.Cell(row, 14).SetValue(item.YG_IcCap_K_Hesap);
                    worksheet.Cell(row, 15).SetValue(item.YG_IcCap_U_Hesap);
                    worksheet.Cell(row, 16).SetValue(item.P0_Garanti);
                    worksheet.Cell(row, 17).SetValue(item.P0_Test);
                    worksheet.Cell(row, 18).SetValue(item.P0_Sapma_HT);
                    worksheet.Cell(row, 19).SetValue(item.Pk_Garanti);
                    worksheet.Cell(row, 20).SetValue(item.Pk_Test);
                    worksheet.Cell(row, 21).SetValue(item.Pk_Sapma_HT);
                    worksheet.Cell(row, 22).SetValue(item.Uk_Garanti);
                    worksheet.Cell(row, 23).SetValue(item.Uk_Test);
                    worksheet.Cell(row, 24).SetValue(item.Uk_Sapma_HT);
                    worksheet.Cell(row, 25).SetValue(item.Sonuc ?? "");
                    row++;
                }

                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Trafo_Raporu_{DateTime.Now:ddMMyyyy}.xlsx");
                }
            }
        }

        public IActionResult Create()
        {
            YukleSelectListler();
            return View(new TrafoTest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrafoTest trafoTest)
        {
            await SetMuhendisIdsAsync(trafoTest);
            if (ModelState.IsValid)
            {
                trafoTest.Hesapla();
                _context.Add(trafoTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            YukleSelectListler();
            return View(trafoTest);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var trafo = await _context.TestKayitlari
                .Include(x => x.ElektrikMuhendisi)
                .Include(x => x.MekanikMuhendisi)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (trafo == null) return NotFound();

            trafo.ElektrikMuhendisiAdSoyad = trafo.ElektrikMuhendisi?.AdSoyad ?? string.Empty;
            trafo.MekanikMuhendisiAdSoyad = trafo.MekanikMuhendisi?.AdSoyad ?? string.Empty;

            YukleSelectListler();
            return View(trafo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrafoTest trafoTest)
        {
            if (id != trafoTest.Id) return NotFound();
            await SetMuhendisIdsAsync(trafoTest);

            var isAjax = string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
            if (!ModelState.IsValid)
            {
                if (isAjax)
                {
                    var errors = ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var firstError = errors
                        .SelectMany(kvp => kvp.Value.Select(msg => new { Field = kvp.Key, Message = msg }))
                        .FirstOrDefault();

                    var message = firstError == null
                        ? "Validasyon hatası."
                        : $"Validasyon hatası: {firstError.Field} - {firstError.Message}";

                    return BadRequest(new { success = false, message, errors });
                }

                YukleSelectListler();
                return View(trafoTest);
            }

            try
            {
                trafoTest.Hesapla();
                _context.Update(trafoTest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TestKayitlari.Any(e => e.Id == trafoTest.Id)) return NotFound();
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (isAjax)
                {
                    return BadRequest(new { success = false, message = "Veritabanına kayıt sırasında hata oluştu.", detail = ex.InnerException?.Message ?? ex.Message });
                }
                throw;
            }

            if (isAjax)
            {
                return Ok(new { success = true });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SaveAs(TrafoTest trafoTest, string YeniProjeAdi)
        {
            trafoTest.Id = 0;
            if (!string.IsNullOrEmpty(YeniProjeAdi))
            {
                trafoTest.ProjeAdi = YeniProjeAdi;
            }

            await SetMuhendisIdsAsync(trafoTest);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Mühendis bilgileri geçersiz." });
            }

            trafoTest.Hesapla();
            _context.Add(trafoTest);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, redirectUrl = Url.Action("Index") });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var trafo = await _context.TestKayitlari.FirstOrDefaultAsync(m => m.Id == id);
            if (trafo == null) return NotFound();
            return View(trafo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isAjax = string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
            var trafo = await _context.TestKayitlari.FindAsync(id);
            if (trafo != null)
            {
                _context.TestKayitlari.Remove(trafo);
                await _context.SaveChangesAsync();

                if (isAjax)
                {
                    return Ok(new { success = true, message = "Kayıt silindi." });
                }
            }

            if (isAjax)
            {
                return NotFound(new { success = false, message = "Silinecek kayıt bulunamadı." });
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> GetTrafolar(string? q)
        {
            var veriler = await FiltrelenmisTrafoSorgusu(q)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return Json(new { data = veriler });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> KaydetPivotTablo([FromBody] PivotTabloKaydetRequest? request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.TabloAdi) || string.IsNullOrWhiteSpace(request.KonfigJson))
            {
                return BadRequest(new { success = false, message = "Tablo adı ve içerik zorunludur." });
            }

            var tabloAdi = request.TabloAdi.Trim();
            if (tabloAdi.Length > 150)
            {
                return BadRequest(new { success = false, message = "Tablo adı en fazla 150 karakter olabilir." });
            }

            await PivotTabloDepolamaTablosunuHazirlaAsync();

            var olusturan = HttpContext.Session.GetString("User")?.Trim();
            if (string.IsNullOrWhiteSpace(olusturan))
            {
                olusturan = "Sistem Kullanıcısı";
            }

            var sql = @"
INSERT INTO dbo.PivotTablolar (Ad, KonfigJson, Olusturan, OlusturmaTarihi)
VALUES (@ad, @konfig, @olusturan, SYSUTCDATETIME());";

            await _context.Database.ExecuteSqlRawAsync(
                sql,
                new SqlParameter("@ad", tabloAdi),
                new SqlParameter("@konfig", request.KonfigJson),
                new SqlParameter("@olusturan", olusturan));

            return Ok(new { success = true, message = "Pivot tablo kaydedildi." });
        }

        [HttpGet]
        public async Task<IActionResult> KayitliPivotTablolar()
        {
            await PivotTabloDepolamaTablosunuHazirlaAsync();

            var kayitlar = new List<object>();
            var connection = _context.Database.GetDbConnection();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                await using var command = connection.CreateCommand();
                command.CommandText = @"
SELECT Id, Ad, Olusturan, OlusturmaTarihi
FROM dbo.PivotTablolar
ORDER BY OlusturmaTarihi DESC, Id DESC;";

                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    kayitlar.Add(new
                    {
                        Id = reader.GetInt32(0),
                        Ad = reader.GetString(1),
                        Olusturan = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        OlusturmaTarihi = reader.GetDateTime(3).ToLocalTime().ToString("yyyy-MM-dd HH:mm")
                    });
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }

            return Ok(new { success = true, data = kayitlar });
        }

        [HttpGet]
        public async Task<IActionResult> PivotTabloDetay(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "Geçersiz kayıt." });
            }

            await PivotTabloDepolamaTablosunuHazirlaAsync();
            var connection = _context.Database.GetDbConnection();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                await using var command = connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 Id, Ad, KonfigJson FROM dbo.PivotTablolar WHERE Id = @id;";

                var param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = id;
                command.Parameters.Add(param);

                await using var reader = await command.ExecuteReaderAsync();
                if (!await reader.ReadAsync())
                {
                    return NotFound(new { success = false, message = "Kayıt bulunamadı." });
                }

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        Id = reader.GetInt32(0),
                        Ad = reader.GetString(1),
                        KonfigJson = reader.GetString(2)
                    }
                });
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> PivotTabloSil([FromBody] PivotTabloSilRequest? request)
        {
            if (request == null || request.Id <= 0)
            {
                return BadRequest(new { success = false, message = "Geçersiz kayıt." });
            }

            await PivotTabloDepolamaTablosunuHazirlaAsync();
            var silinen = await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM dbo.PivotTablolar WHERE Id = @id;",
                new SqlParameter("@id", request.Id));

            if (silinen == 0)
            {
                return NotFound(new { success = false, message = "Silinecek kayıt bulunamadı." });
            }

            return Ok(new { success = true, message = "Kayıt silindi." });
        }

        private IQueryable<TrafoTest> FiltrelenmisTrafoSorgusu(string? q)
        {
            var sorgu = _context.TestKayitlari.AsQueryable();
            var aramaMetni = q?.Trim();

            if (string.IsNullOrWhiteSpace(aramaMetni))
            {
                return sorgu;
            }

            return sorgu.Where(x =>
                (x.ProjeAdi != null && x.ProjeAdi.Contains(aramaMetni)) ||
                (x.Musteri != null && x.Musteri.Contains(aramaMetni)) ||
                (x.TasarimNo != null && x.TasarimNo.Contains(aramaMetni)));
        }

        private async Task PivotTabloDepolamaTablosunuHazirlaAsync()
        {
            const string sql = @"
IF OBJECT_ID(N'dbo.PivotTablolar', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PivotTablolar
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Ad NVARCHAR(150) NOT NULL,
        KonfigJson NVARCHAR(MAX) NOT NULL,
        Olusturan NVARCHAR(150) NULL,
        OlusturmaTarihi DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );
END";

            await _context.Database.ExecuteSqlRawAsync(sql);
        }

        public sealed class PivotTabloKaydetRequest
        {
            public string TabloAdi { get; set; } = string.Empty;
            public string KonfigJson { get; set; } = string.Empty;
        }

        public sealed class PivotTabloSilRequest
        {
            public int Id { get; set; }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AkilliExcelAktar(string? dosyaYolu = null)
        {
            var varsayilanYol = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads",
                "sonuncu excel.xlsx");

            var hedefYol = string.IsNullOrWhiteSpace(dosyaYolu) ? varsayilanYol : dosyaYolu.Trim();

            if (!System.IO.File.Exists(hedefYol))
            {
                return NotFound(new
                {
                    success = false,
                    message = "Excel dosyası bulunamadı.",
                    dosyaYolu = hedefYol
                });
            }

            var sonuc = await ExceldenAkilliAktarAsync(hedefYol);
            return Ok(sonuc);
        }

        private async Task<object> ExceldenAkilliAktarAsync(string dosyaYolu)
        {
            var musteriHavuzu = new[]
            {
                "Anadolu Enerji A.Ş.", "Marmara Elektrik Sanayi", "Ege Trafo Sistemleri", "Boğaziçi Endüstri",
                "Bursa Güç Teknolojileri", "Karadeniz Dağıtım", "Akdeniz Şebeke Çözümleri", "İç Anadolu Mekatronik",
                "Doğu Enerji Yatırımları", "Trakya Mühendislik Grubu"
            };

            var muhendisHavuzu = new[]
            {
                "Ahmet Yılmaz", "Mehmet Demir", "Ayşe Kaya", "Zeynep Çelik", "Murat Şahin"
            };

            var otomatikProjeAdlari = new[]
            {
                "Anadolu Güç Projesi", "Marmara Trafo Projesi", "Ege Şebeke Projesi", "Boğaziçi Enerji Projesi",
                "Bursa Dağıtım Projesi", "Karadeniz Güç Projesi", "Akdeniz Trafo Projesi", "İç Anadolu Enerji Projesi",
                "Doğu Şebeke Projesi", "Trakya Güç Projesi"
            };

            await MuhendisVeKullaniciKayitlariniHazirlaAsync(muhendisHavuzu);

            var muhendisIdMap = await _context.Muhendisler
                .Where(x => muhendisHavuzu.Contains(x.AdSoyad))
                .ToDictionaryAsync(x => x.AdSoyad, x => x.Id);

            var cekirdekTipiId = await _context.CekirdekTipleri.OrderBy(x => x.Id).Select(x => x.Id).FirstAsync();
            var sacCinsiId = await _context.SacCinsleri.OrderBy(x => x.Id).Select(x => x.Id).FirstAsync();
            var kazanCinsiId = await _context.KazanCinsleri.OrderBy(x => x.Id).Select(x => x.Id).FirstAsync();
            var yagCinsiId = await _context.YagCinsleri.OrderBy(x => x.Id).Select(x => x.Id).FirstAsync();

            var rastgele = new Random();
            var testListesi = new List<TrafoTest>();
            var benzersizSatirlar = new HashSet<string>(StringComparer.Ordinal);

            using var workbook = new XLWorkbook(dosyaYolu);
            var worksheet = workbook.Worksheets.First();
            var headerRow = worksheet.FirstRowUsed();

            if (headerRow == null)
            {
                return new
                {
                    success = false,
                    message = "Excel dosyasında başlık satırı bulunamadı.",
                    eklenenKayit = 0,
                    tekrarAtlanan = 0,
                    dosyaYolu
                };
            }

            var headerMap = new Dictionary<int, string>();
            foreach (var cell in headerRow.CellsUsed())
            {
                headerMap[cell.Address.ColumnNumber] = cell.GetValue<string>().Trim();
            }

            var projeAdiKolon = BulSutun(headerMap, "projeadi", "proje");
            if (projeAdiKolon == 0)
            {
                return new
                {
                    success = false,
                    message = "PROJE ADI sütunu bulunamadı.",
                    eklenenKayit = 0,
                    tekrarAtlanan = 0,
                    dosyaYolu
                };
            }

            var gucKolon = BulSutun(headerMap, "guc", "guckva", "guckva");
            var ygGerilimKolon = BulSutun(headerMap, "yggerilimi", "yggerilim", "ygv");
            var agGerilimKolon = BulSutun(headerMap, "aggerilimi", "aggerilim", "agv");
            var p0OlculenKolon = BulSutun(headerMap, "p0olculen", "polculen", "p0test");
            var pkOlculenKolon = BulSutun(headerMap, "pkolculen", "pktest");
            var ukOlculenKolon = BulSutun(headerMap, "ukolculen", "ukolculen", "uktest");

            var tekrarAtlanan = 0;
            var satirIndex = 0;
            var veriSatirlari = worksheet.RowsUsed().Skip(1);

            foreach (var row in veriSatirlari)
            {
                satirIndex++;
                var projeAdi = row.Cell(projeAdiKolon).GetValue<string>().Trim();
                if (string.IsNullOrWhiteSpace(projeAdi))
                {
                    var adKoku = otomatikProjeAdlari[(satirIndex - 1) % otomatikProjeAdlari.Length];
                    projeAdi = $"{adKoku} {satirIndex:000}";
                }

                var satirSozluk = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var kvp in headerMap)
                {
                    var hucreDegeri = row.Cell(kvp.Key).GetValue<string>().Trim();
                    satirSozluk[kvp.Value] = hucreDegeri;
                }

                var tekrarImzasi = SatirTekrarImzasiUret(satirSozluk);
                if (!benzersizSatirlar.Add(tekrarImzasi))
                {
                    tekrarAtlanan++;
                    continue;
                }

                var guc = SayisalDegerCoz(row, gucKolon);
                var ygGerilim = SayisalDegerCoz(row, ygGerilimKolon);
                var agGerilim = SayisalDegerCoz(row, agGerilimKolon);
                var p0Olculen = SayisalDegerCoz(row, p0OlculenKolon);
                var pkOlculen = SayisalDegerCoz(row, pkOlculenKolon);
                var ukOlculen = SayisalDegerCoz(row, ukOlculenKolon);

                var secilenMusteri = musteriHavuzu[(satirIndex - 1) % musteriHavuzu.Length];
                var elektrikMuhendisiAd = muhendisHavuzu[rastgele.Next(muhendisHavuzu.Length)];
                var mekanikMuhendisiAd = muhendisHavuzu[rastgele.Next(muhendisHavuzu.Length)];

                var p0Hesap = HesapDegeriUret(p0Olculen, rastgele);
                var pkHesap = HesapDegeriUret(pkOlculen, rastgele);
                var ukHesap = HesapDegeriUret(ukOlculen, rastgele);

                var test = new TrafoTest
                {
                    ProjeAdi = projeAdi,
                    Musteri = secilenMusteri,
                    TasarimNo = $"TSR-{DateTime.Now:yyyy}-{satirIndex:0000}",
                    DizaynId = $"DZ-{DateTime.Now:yy}{satirIndex:0000}",
                    DizaynTarihi = DateTime.Now.AddDays(-rastgele.Next(25, 240)),
                    TestTarihi = DateTime.Now.AddDays(-rastgele.Next(1, 24)),
                    ElektrikMuhendisiId = muhendisIdMap[elektrikMuhendisiAd],
                    MekanikMuhendisiId = muhendisIdMap[mekanikMuhendisiAd],
                    ElektrikMuhendisiAdSoyad = elektrikMuhendisiAd,
                    MekanikMuhendisiAdSoyad = mekanikMuhendisiAd,
                    Guc = guc,
                    GerilimYG = ygGerilim,
                    GerilimAG = agGerilim > 0 ? agGerilim : 400,
                    BaglantiGrubu = "Dyn11",
                    Frekans = 50,
                    CekirdekTipiId = cekirdekTipiId,
                    SacCinsiId = sacCinsiId,
                    KazanCinsiId = kazanCinsiId,
                    YagCinsiId = yagCinsiId,
                    P0_Tolerans = 15,
                    Pk_Tolerans = 15,
                    Uk_Tolerans = 10,
                    P0_Garanti = p0Hesap,
                    P0_Hesap = p0Hesap,
                    P0_Test = p0Olculen,
                    Pk_Garanti = pkHesap,
                    Pk_Hesap = pkHesap,
                    Pk_Test = pkOlculen,
                    Uk_Garanti = ukHesap,
                    Uk_Hesap = ukHesap,
                    Uk_Test = ukOlculen
                };

                test.Hesapla();
                testListesi.Add(test);
            }

            if (testListesi.Count > 0)
            {
                _context.TestKayitlari.AddRange(testListesi);
                await _context.SaveChangesAsync();
            }

            return new
            {
                success = true,
                message = "Akıllı Excel aktarımı tamamlandı.",
                eklenenKayit = testListesi.Count,
                tekrarAtlanan,
                musteriCesidi = musteriHavuzu.Length,
                muhendisCesidi = muhendisHavuzu.Length,
                dosyaYolu
            };
        }

        private async Task MuhendisVeKullaniciKayitlariniHazirlaAsync(string[] muhendisAdlari)
        {
            var mevcutMuhendisler = await _context.Muhendisler
                .Where(x => muhendisAdlari.Contains(x.AdSoyad))
                .Select(x => x.AdSoyad)
                .ToListAsync();

            var eklenecekMuhendisler = muhendisAdlari
                .Where(x => !mevcutMuhendisler.Contains(x))
                .Select(x => new Muhendis { AdSoyad = x })
                .ToList();

            if (eklenecekMuhendisler.Count > 0)
            {
                _context.Muhendisler.AddRange(eklenecekMuhendisler);
            }

            var mevcutKullanicilar = await _context.Kullanicilar
                .Where(x => muhendisAdlari.Contains(x.AdSoyad))
                .Select(x => x.AdSoyad)
                .ToListAsync();

            var eklenecekKullanicilar = new List<Kullanici>();
            foreach (var adSoyad in muhendisAdlari)
            {
                if (mevcutKullanicilar.Contains(adSoyad))
                {
                    continue;
                }

                var temizKullaniciAdi = Regex.Replace(TurkceyiTemizle(adSoyad), "[^a-zA-Z0-9]", "").ToLowerInvariant();
                if (string.IsNullOrWhiteSpace(temizKullaniciAdi))
                {
                    temizKullaniciAdi = $"muhendis{Guid.NewGuid():N}".Substring(0, 12);
                }

                eklenecekKullanicilar.Add(new Kullanici
                {
                    AdSoyad = adSoyad,
                    KullaniciAdi = temizKullaniciAdi,
                    Email = $"{temizKullaniciAdi}@ornekfirma.com",
                    Sifre = "123456"
                });
            }

            if (eklenecekKullanicilar.Count > 0)
            {
                _context.Kullanicilar.AddRange(eklenecekKullanicilar);
            }

            if (eklenecekMuhendisler.Count > 0 || eklenecekKullanicilar.Count > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        private static int BulSutun(Dictionary<int, string> headerMap, params string[] adaylar)
        {
            var normalizeAdaylar = adaylar
                .Select(x => TurkceyiTemizle(x).Replace(" ", string.Empty).ToLowerInvariant())
                .ToList();

            foreach (var kvp in headerMap)
            {
                var normalizedHeader = TurkceyiTemizle(kvp.Value)
                    .Replace(" ", string.Empty)
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty)
                    .Replace("_", string.Empty)
                    .ToLowerInvariant();

                if (normalizeAdaylar.Any(a => normalizedHeader.Contains(a)))
                {
                    return kvp.Key;
                }
            }

            return 0;
        }

        private static string SatirTekrarImzasiUret(Dictionary<string, string> satirSozluk)
        {
            var haricTutulan = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "siparis kodlari",
                "sipariş kodlari",
                "sipariş kodları",
                "siparis kodlari",
                "seri no",
                "seri numarasi",
                "seri numarası"
            };

            var builder = new StringBuilder();
            foreach (var item in satirSozluk
                .OrderBy(x => x.Key, StringComparer.OrdinalIgnoreCase))
            {
                var normalizedKey = TurkceyiTemizle(item.Key).ToLowerInvariant();
                if (haricTutulan.Contains(normalizedKey))
                {
                    continue;
                }

                // Olculen/test alanlarindaki farklar ayni projenin tekrarini bozmasin.
                if (normalizedKey.Contains("olculen") || normalizedKey.Contains("test"))
                {
                    continue;
                }

                builder.Append(normalizedKey.Trim())
                    .Append('=')
                    .Append(item.Value.Trim())
                    .Append('|');
            }

            return builder.ToString();
        }

        private static double SayisalDegerCoz(IXLRow row, int kolon)
        {
            if (kolon <= 0)
            {
                return 0;
            }

            var ham = row.Cell(kolon).GetValue<string>();
            return MetindenSayisalDegerCoz(ham);
        }

        private static double MetindenSayisalDegerCoz(string? ham)
        {
            if (string.IsNullOrWhiteSpace(ham))
            {
                return 0;
            }

            var temiz = ham
                .Replace("kVA", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("KV", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("V", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("%", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Trim();

            temiz = Regex.Replace(temiz, @"[^0-9,.-]", string.Empty);

            if (string.IsNullOrWhiteSpace(temiz))
            {
                return 0;
            }

            if (double.TryParse(temiz, NumberStyles.Any, new CultureInfo("tr-TR"), out var trDeger))
            {
                return trDeger;
            }

            if (double.TryParse(temiz, NumberStyles.Any, CultureInfo.InvariantCulture, out var invDeger))
            {
                return invDeger;
            }

            return 0;
        }

        private static double HesapDegeriUret(double testDegeri, Random rastgele)
        {
            if (testDegeri <= 0)
            {
                return 1;
            }

            var katsayi = 0.93 + (rastgele.NextDouble() * 0.12);
            return Math.Round(testDegeri * katsayi, 2);
        }

        private static string TurkceyiTemizle(string? metin)
        {
            if (string.IsNullOrWhiteSpace(metin))
            {
                return string.Empty;
            }

            return metin
                .Replace('ç', 'c').Replace('Ç', 'C')
                .Replace('ğ', 'g').Replace('Ğ', 'G')
                .Replace('ı', 'i').Replace('İ', 'I')
                .Replace('ö', 'o').Replace('Ö', 'O')
                .Replace('ş', 's').Replace('Ş', 'S')
                .Replace('ü', 'u').Replace('Ü', 'U');
        }

        private void YukleSelectListler()
        {
            ViewBag.CekirdekTipleri = _context.CekirdekTipleri.OrderBy(x => x.Id).Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Ad }).ToList();
            ViewBag.SacCinsleri = _context.SacCinsleri.OrderBy(x => x.Id).Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Ad }).ToList();
            ViewBag.KazanCinsleri = _context.KazanCinsleri.OrderBy(x => x.Id).Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Ad }).ToList();
            ViewBag.YagCinsleri = _context.YagCinsleri.OrderBy(x => x.Id).Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Ad }).ToList();
            ViewBag.MuhendisListesi = _context.Kullanicilar
                .OrderBy(x => x.AdSoyad)
                .Select(x => x.AdSoyad)
                .Distinct()
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x,
                    Text = x
                })
                .ToList();
        }

        private async Task SetMuhendisIdsAsync(TrafoTest trafoTest)
        {
            var kullaniciAdlari = await _context.Kullanicilar
                .Select(x => x.AdSoyad)
                .Distinct()
                .ToListAsync();

            if (string.IsNullOrWhiteSpace(trafoTest.ElektrikMuhendisiAdSoyad))
            {
                ModelState.AddModelError(nameof(TrafoTest.ElektrikMuhendisiAdSoyad), "Elektrik mühendisi zorunludur.");
            }
            else if (!kullaniciAdlari.Contains(trafoTest.ElektrikMuhendisiAdSoyad.Trim()))
            {
                ModelState.AddModelError(nameof(TrafoTest.ElektrikMuhendisiAdSoyad), "Elektrik mühendisi listeden seçilmelidir.");
            }
            else
            {
                trafoTest.ElektrikMuhendisiId = await GetOrCreateMuhendisIdAsync(trafoTest.ElektrikMuhendisiAdSoyad);
            }

            if (string.IsNullOrWhiteSpace(trafoTest.MekanikMuhendisiAdSoyad))
            {
                ModelState.AddModelError(nameof(TrafoTest.MekanikMuhendisiAdSoyad), "Mekanik mühendisi zorunludur.");
            }
            else if (!kullaniciAdlari.Contains(trafoTest.MekanikMuhendisiAdSoyad.Trim()))
            {
                ModelState.AddModelError(nameof(TrafoTest.MekanikMuhendisiAdSoyad), "Mekanik mühendisi listeden seçilmelidir.");
            }
            else
            {
                trafoTest.MekanikMuhendisiId = await GetOrCreateMuhendisIdAsync(trafoTest.MekanikMuhendisiAdSoyad);
            }
        }

        private async Task<int> GetOrCreateMuhendisIdAsync(string adSoyad)
        {
            var normalized = adSoyad.Trim();
            var existing = await _context.Muhendisler
                .Where(x => x.AdSoyad == normalized)
                .Select(x => new { x.Id })
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                return existing.Id;
            }

            var muhendis = new Muhendis { AdSoyad = normalized };
            _context.Muhendisler.Add(muhendis);
            await _context.SaveChangesAsync();
            return muhendis.Id;
        }
    }
} 