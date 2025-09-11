using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    // Concrete: Abstract katmandaki arayüzün (IPostRepository) EF Core ile gerçek uygulaması
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context; // EF Core DbContext: veritabanı bağlantısı ve DbSet'ler burada
        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }
        // Controller tarafına sorgulanabilir Post kaynağı sunar (henüz DB'ye gitmez, IQueryable)
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            // Yeni post'u ekleyip veritabanına yazar
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}