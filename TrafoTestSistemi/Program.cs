using Microsoft.EntityFrameworkCore;
using TrafoTestSistemi.Models;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Baðlantýsý
builder.Services.AddDbContext<TrafoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. MVC Servisleri
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// --- YENÝ EKLEME: Session (Oturum) Servisi ---
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika iþlem yapýlmazsa oturum düþer
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
// --------------------------------------------

var app = builder.Build();

// 3. Hata ayýklama ve HTTPS yönlendirmeleri
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- YENÝ EKLEME: Session Kullanýmýný Aktifleþtir ---
app.UseSession();
// --------------------------------------------------

app.UseAuthorization();

// 4. Uygulama açýldýðýnda artýk Index deðil, ACCOUNT/LOGIN gelecek
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();