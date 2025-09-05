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



        // Güncelleme formu için id vasıtasıyla veritabanında ilgili kursun bulunup form üzerinde gösterilmesi.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] // Formu get metoduyla görüntüleyen kişi ile post metoduyla güncelleyen kişinin aynı olup olmadığını token bilgisiyle kontrol eder. (Cross-side attack)
        public async Task<IActionResult> Edit(int id, Kurs model)
        {
            if (id != model.KursId)
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
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
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

            var kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}