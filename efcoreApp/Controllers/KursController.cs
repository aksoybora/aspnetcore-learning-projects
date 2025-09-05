using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        // Injection Yöntemi
        private readonly DataContext _context; // DataContext referansı

        public KursController(DataContext context) // DataContext referansı
        {
            _context = context;  // Gelen context'i sakla
        }



        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }


        public IActionResult Create()
        {
            return View();
        }
        


        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model); // DataContext üzerinden ekle
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet


            return RedirectToAction("Index", "Kurs");
        }
    }
}