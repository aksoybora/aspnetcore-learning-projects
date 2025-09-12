using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        // Repository bağımlılıklarını DI üzerinden alıyoruz
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index(string tag)
        {
            var claims = User.Claims; // Giriş yapan kullanıcının claim bilgileri
            var posts = _postRepository.Posts; // Tüm post sorgusu (IQueryable)

            if(!string.IsNullOrEmpty(tag))
            {
                // Etiket filtresi: URL eşleşen etikete sahip postları getir
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            // ViewModel ile listeyi view'a gönderiyoruz
            return View( new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url)
        {
            // İlgili postu etiketleri ve yorumları (yorumun kullanıcısı ile) birlikte yüklüyoruz
            return View(await _postRepository
                        .Posts
                        .Include(x => x.Tags)
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)
                        .FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string UserName, string Text)
        {
            // Yeni yorum nesnesi oluşturup post ile ilişkilendiriyoruz
            var entity = new Comment {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                // Basitlik için sadece kullanıcı adı ve varsayılan görsel atanıyor
                User = new User { UserName = UserName, Image = "avatar.jpg" }
            };
            _commentRepository.CreateComment(entity);

            // AJAX çağrısı için geri dönen minimal veri
            return Json(new { 
                UserName,
                Text,
                entity.PublishedOn,
                entity.User.Image
            });

        }
    }
}