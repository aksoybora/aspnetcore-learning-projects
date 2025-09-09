using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgretmenController : Controller
    {

        // Injection Yöntemi
        private readonly DataContext _context; // DataContext referansı

        public OgretmenController(DataContext context) // DataContext referansı
        {
            _context = context;  // Gelen context'i sakla
        }

        public async Task<IActionResult> Index()
        {
            var ogretmenler = await _context.Ogretmenler.ToListAsync();
            return View(ogretmenler);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            _context.Ogretmenler.Add(model); // DataContext üzerinden ekle
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet


            return RedirectToAction("Index", "Ogretmen");
        }

        // Güncelleme formu için id vasıtasıyla veritabanında ilgili öğrencininn bulunup form üzerinde gösterilmesi.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogretmen = await _context
                            .Ogretmenler
                            .Include(o => o.Kurslar)
                            .FirstOrDefaultAsync(o => o.OgretmenId == id);

            if (ogretmen == null)
            {
                return NotFound();
            }

            return View(ogretmen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Formu get metoduyla görüntüleyen kişi ile post metoduyla güncelleyen kişinin aynı olup olmadığını token bilgisiyle kontrol eder. (Cross-side attack)
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silinecekOgretmen = await _context.Ogretmenler.FindAsync(id);

            if (silinecekOgretmen == null)
            {
                return NotFound();
            }

            return View(silinecekOgretmen);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silinecekOgretmen = await _context.Ogretmenler.FindAsync(id);

            if (silinecekOgretmen == null)
            {
                return NotFound();
            }

            _context.Ogretmenler.Remove(silinecekOgretmen);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}