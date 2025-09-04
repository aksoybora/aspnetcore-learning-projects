using MeetingApp.Models; // Models klasöründeki sınıfları kullanabilmek için import
using Microsoft.AspNetCore.Mvc; // MVC framework'ün temel sınıflarını import

namespace MeetingApp.Controllers
{
    // HomeController sınıfı - ana sayfa işlemlerini yönetir
    // Controller sınıfından türetilmiş - MVC pattern'in Controller katmanı
    public class HomeController: Controller 
    {
        // Index action metodu - ana sayfa için
        // IActionResult: Action'ın döndürebileceği farklı sonuç türleri (View, Json, Redirect vb.)
        public IActionResult Index()
        {
            // Şu anki saati alıyoruz
            int saat = DateTime.Now.Hour;

            // Saate göre selamlama mesajı belirleme
            // ViewData: Controller'dan View'a veri aktarmak için kullanılan dictionary
            // Ternary operator: saat > 12 ise "İyi Günler", değilse "Günaydın"
            ViewData["Selamlama"] = saat > 12 ? "İyi Günler":"Günaydın";
            
            // Repository'den katılımcı sayısını alma
            // LINQ kullanarak WillAttend = true olan kullanıcıları sayıyoruz
            int UserCount = Repository.Users.Where(info=> info.WillAttend == true).Count();

            // MeetingInfo nesnesi oluşturma - toplantı bilgileri
            var meetingInfo = new MeetingInfo() 
            {
                Id = 1,
                Location = "İstanbul, Abc Kongre Merkezi", // Toplantı yeri
                Date = new DateTime(2024, 01, 20, 20, 0, 0), // Toplantı tarihi ve saati
                NumberOfPeople = UserCount // Katılımcı sayısı
            };

            // YENİ: İstatistik bilgilerini ViewData'ya ekleme
            ViewData["Statistics"] = Repository.GetStatistics();
            ViewData["Favorites"] = Repository.GetFavorites();

            // View'a meetingInfo modelini göndererek sayfayı render etme
            return View(meetingInfo);
        }

        // YENİ: İstatistik sayfası için action
        public IActionResult Statistics()
        {
            var stats = Repository.GetStatistics();
            return View(stats);
        }

        // YENİ: Favori kullanıcılar sayfası için action
        public IActionResult Favorites()
        {
            var favorites = Repository.GetFavorites();
            return View(favorites);
        }
    }
}