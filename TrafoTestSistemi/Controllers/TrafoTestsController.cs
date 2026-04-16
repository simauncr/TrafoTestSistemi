using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrafoTestSistemi.Models;
using ClosedXML.Excel;
using System.IO;
using System.Linq;
using System;
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

        public async Task<IActionResult> Index()
        {
            var veriler = await _context.TestKayitlari.ToListAsync();
            return View(veriler);
        }

        public async Task<IActionResult> Analiz()
        {
            var testler = await _context.TestKayitlari.ToListAsync();
            ViewBag.Toplam = testler.Count;
            ViewBag.Uygun = testler.Count(x => x.Sonuc != null && x.Sonuc.ToUpper().Trim() == "UYGUN");
            ViewBag.UygunDegil = testler.Count(x => x.Sonuc != null && (x.Sonuc.ToUpper().Trim().Contains("DEĞİL") || x.Sonuc.ToUpper().Trim() == "HATALI"));

            if (ViewBag.Toplam > 0 && (int)ViewBag.Uygun == 0 && (int)ViewBag.UygunDegil == 0)
            {
                ViewBag.UygunDegil = ViewBag.Toplam;
            }
            return View();
        }

        public async Task<IActionResult> ExcelIndir()
        {
            var veriler = await _context.TestKayitlari.ToListAsync();
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
                    worksheet.Cell(row, 5).SetValue(item.ElektrikMuhendisi ?? "");
                    worksheet.Cell(row, 6).SetValue(item.MekanikMuhendisi ?? "");
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrafoTest trafoTest)
        {
            if (ModelState.IsValid)
            {
                trafoTest.Hesapla();
                _context.Add(trafoTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trafoTest);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var trafo = await _context.TestKayitlari.FindAsync(id);
            if (trafo == null) return NotFound();
            return View(trafo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrafoTest trafoTest)
        {
            if (id != trafoTest.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    trafoTest.Hesapla();
                    _context.Update(trafoTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TestKayitlari.Any(e => e.Id == trafoTest.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trafoTest);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAs(TrafoTest trafoTest, string YeniProjeAdi)
        {
            trafoTest.Id = 0;
            if (!string.IsNullOrEmpty(YeniProjeAdi))
            {
                trafoTest.ProjeAdi = YeniProjeAdi;
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
            var trafo = await _context.TestKayitlari.FindAsync(id);
            if (trafo != null)
            {
                _context.TestKayitlari.Remove(trafo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));


        }

        [HttpGet]
        public async Task<IActionResult> GetTrafolar()
        {
            var veriler = await _context.TestKayitlari
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return Json(new { data = veriler });
        }
    }
} 