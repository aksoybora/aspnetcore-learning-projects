using Microsoft.AspNetCore.Mvc; // MVC framework'ün temel sınıflarını import
using MeetingApp.Models; // Models klasöründeki sınıfları kullanabilmek için import

namespace MeetingApp.Controllers
{
    // MeetingController sınıfı - toplantı işlemlerini yönetir
    // Controller sınıfından türetilmiş - MVC pattern'in Controller katmanı
    public class MeetingController : Controller
    {
        // [HttpGet] attribute'u - bu action'ın sadece GET request'lerde çalışacağını belirtir
        // Apply action metodu - toplantıya katılım formunu gösterir
        [HttpGet]
        public IActionResult Apply()
        {
            // Boş form sayfasını gösterir
            return View();
        }

        // [HttpPost] attribute'u - bu action'ın sadece POST request'lerde çalışacağını belirtir
        // Apply action metodu - form gönderildiğinde çalışır
        // UserInfo model: Form'dan gelen verileri otomatik olarak bu nesneye bind eder
        [HttpPost]
        public IActionResult Apply(UserInfo model)
        {
            // ModelState.IsValid: Form validasyonunun başarılı olup olmadığını kontrol eder
            if(ModelState.IsValid) {
                // Form verileri geçerliyse, Repository'ye yeni kullanıcı ekleme
                Repository.CreateUser(model);
                
                // Katılımcı sayısını güncelleme ve ViewBag ile View'a aktarma
                // ViewBag: Controller'dan View'a veri aktarmak için kullanılan dynamic object
                ViewBag.UserCount = Repository.Users.Where(info=> info.WillAttend == true).Count();
                
                // Thanks view'ını gösterme ve model verisini aktarma
                return View("Thanks", model);
            } else {
                // Form verileri geçersizse, aynı sayfayı tekrar gösterme
                // Model verisi de gönderilir ki kullanıcı yazdıklarını kaybetmesin
                return View(model);
            }
        }

        // [HttpGet] attribute'u - bu action'ın sadece GET request'lerde çalışacağını belirtir
        // List action metodu - tüm katılımcıları listeler
        [HttpGet]
        public IActionResult List()
        {
            // Repository'den tüm kullanıcıları alıp View'a gönderme
            return View(Repository.Users);
        }

        // Details action metodu - belirli bir kullanıcının detaylarını gösterir
        // Route: meeting/details/2 şeklinde çağrılır (2 = id parametresi)
        public IActionResult Details(int id)
        {
            // Repository'den ID'ye göre kullanıcı bulma ve View'a gönderme
            return View(Repository.GetById(id));
        }

        // YENİ: Favori durumunu değiştirme action'ı
        [HttpPost]
        public IActionResult ToggleFavorite(int id)
        {
            try
            {
                Repository.ToggleFavorite(id);
                var user = Repository.GetById(id);
                
                return Json(new { 
                    success = true, 
                    isFavorite = user?.IsFavorite ?? false 
                });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        // YENİ: Kullanıcı notu güncelleme action'ı
        [HttpPost]
        public IActionResult UpdateNote(int id, [FromBody] NoteUpdateModel model)
        {
            try
            {
                Repository.UpdateNote(id, model.Note);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }

    // YENİ: Not güncelleme için model sınıfı
    public class NoteUpdateModel
    {
        public string Note { get; set; } = string.Empty;
    }
}