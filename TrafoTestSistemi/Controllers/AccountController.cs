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
                return RedirectToAction("Index", "TrafoTests");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}