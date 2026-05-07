using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrafoTestSistemi.Models;
using ClosedXML.Excel;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
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