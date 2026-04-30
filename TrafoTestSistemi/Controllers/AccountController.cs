using Microsoft.AspNetCore.Mvc;
using TrafoTestSistemi.Models;
using Microsoft.AspNetCore.Http; 

namespace TrafoTestSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly TrafoContext _context;

        public AccountController(TrafoContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);

            if (user != null)
            {
                HttpContext.Session.SetString("User", user.AdSoyad);
                HttpContext.Session.SetString("UserName", user.KullaniciAdi);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "TrafoTests");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        public IActionResult ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login");
            }

            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var userIdValue = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdValue) || !int.TryParse(userIdValue, out var userId))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Kullanicilar.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (user.Sifre != model.CurrentPassword)
            {
                ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword), "Mevcut şifre hatalı.");
                return View(model);
            }

            if (model.CurrentPassword == model.NewPassword)
            {
                ModelState.AddModelError(nameof(ChangePasswordViewModel.NewPassword), "Yeni şifre mevcut şifre ile aynı olamaz.");
                return View(model);
            }

            user.Sifre = model.NewPassword;
            _context.SaveChanges();

            TempData["Basari"] = "Şifreniz başarıyla güncellendi.";
            return RedirectToAction(nameof(ChangePassword));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}