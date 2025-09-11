using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository; // Repository arayüzü: Controller veri erişim detayını bilmez
        public PostsController(IPostRepository postRepository)
        {
            // DI (Bağımlılık Enjeksiyonu): IPostRepository somut karşılığı (EfPostRepository) burada otomatik verilir
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index(string tag)
        {
            // IQueryable ile başlayan sorgu: henüz DB'ye gitmez, zincirlenebilir
            var posts = _postRepository.Posts;

            if (!string.IsNullOrEmpty(tag))
            {
                // Etiket filtresi: İlgili Tag.Url eşleşen post'ları getir
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            // ToListAsync çağrısıyla sorgu çalışır ve sonuçlar ViewModel'e konur
            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url)
        {
            // Include/ThenInclude: Post ile ilişkili Tag ve Comment -> User verilerini birlikte yükler
            return View(await _postRepository
                        .Posts
                        .Include(x => x.Tags)
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)
                        .FirstOrDefaultAsync(p => p.Url == url));
        }
        
        public IActionResult AddComment(int PostId, string UserName, string Text)
        {
            // Henüz uygulanmamış aksiyon: Formdan yorum alıp kaydetmek için kullanılacak
            return View();
        }
    }
}