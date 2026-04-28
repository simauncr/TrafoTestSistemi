using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrafoTestSistemi.Models;

namespace TrafoTestSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly TrafoContext _context;

        public KullaniciController(TrafoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kullanicilar = await _context.Kullanicilar.ToListAsync();
            return View(kullanicilar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kullanici kullanici)
        {
            if (!ModelState.IsValid)
                return View(kullanici);

            _context.Kullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();

            TempData["Basari"] = $"'{kullanici.AdSoyad}' adlı kullanıcı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
                return NotFound();

            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kullanici kullanici)
        {
            if (id != kullanici.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(kullanici);

            try
            {
                _context.Update(kullanici);
                await _context.SaveChangesAsync();
                TempData["Basari"] = $"'{kullanici.AdSoyad}' adlı kullanıcı başarıyla güncellendi.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Kullanicilar.AnyAsync(k => k.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var kullanici = await _context.Kullanicilar.FirstOrDefaultAsync(k => k.Id == id);
            if (kullanici == null)
                return NotFound();

            return View(kullanici);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();
                TempData["Basari"] = "Kullanıcı başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
