using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {

        // Injection Yöntemi
        private readonly DataContext _context; // DataContext referansı

        public OgrenciController(DataContext context) // DataContext referansı
        {
            _context = context;  // Gelen context'i sakla
        }



        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model); // DataContext üzerinden ekle
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet


            return RedirectToAction("Index", "Ogrenci");
        }


        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }


        // Güncelleme formu için id vasıtasıyla veritabanında ilgili öğrencininn bulunup form üzerinde gösterilmesi.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context
                            .Ogrenciler
                            .Include(o => o.KursKayitları)
                            .ThenInclude(o => o.Kurs)
                            .FirstOrDefaultAsync(o => o.OgrenciId == id);

            if (ogr == null)
            {
                return NotFound();
            }

            return View(ogr);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] // Formu get metoduyla görüntüleyen kişi ile post metoduyla güncelleyen kişinin aynı olup olmadığını token bilgisiyle kontrol eder. (Cross-side attack)
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
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
                    if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
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

            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }




    }
}