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

        public void EditPost(Post post)
        {
            var entity = _context.Posts.FirstOrDefault(i => i.PostId == post.PostId);

            if(entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;

                _context.SaveChanges();
            }
        }
    }
}