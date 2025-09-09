using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using efcoreApp.Models;

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
            var kurslar = await _context.Kurslar.Include(k => k.Ogretmen).ToListAsync();
            return View(kurslar);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return RedirectToAction("Index", "Kurs");
        }



        // Güncelleme formu için id vasıtasıyla veritabanında ilgili kursun bulunup form üzerinde gösterilmesi.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context
                            .Kurslar
                            .Include(k => k.KursKayitları)
                            .ThenInclude(k => k.Ogrenci)
                            .Select(k => new KursViewModel
                            {
                                KursId = k.KursId,
                                Baslik = k.Baslik,
                                OgretmenId = k.OgretmenId,
                                KursKayitları = k.KursKayitları
                            })
                            .FirstOrDefaultAsync(k => k.KursId == id);

            if (kurs == null)
            {
                return NotFound();
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(kurs);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] // Formu get metoduyla görüntüleyen kişi ile post metoduyla güncelleyen kişinin aynı olup olmadığını token bilgisiyle kontrol eder. (Cross-side attack)
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs {KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId});
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

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
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