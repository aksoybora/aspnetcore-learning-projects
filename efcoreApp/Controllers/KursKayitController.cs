using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers
{
    public class KursKayitController : Controller
    {
        // Injection Yöntemi
        private readonly DataContext _context; // DataContext referansı

        public KursKayitController(DataContext context) // DataContext referansı
        {
            _context = context;  // Gelen context'i sakla
        }


        public async Task<IActionResult> Index()
        {
            var kursKayitları = await _context
                                .KursKayitlari
                                .Include(x => x.Ogrenci) // Kurs kayıtları içinden öğrenciye eriş
                                .Include(x => x.Kurs) // Kurs kayıtları içinden kursa eriş
                                .ToListAsync();

            return View(kursKayitları);
        }



        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");

            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        } 



    }
}