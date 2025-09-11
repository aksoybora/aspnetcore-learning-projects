using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    // Concrete: ITagRepository'nin EF Core kullanılarak yapılan gerçek uygulaması
    public class EfTagRepository : ITagRepository
    {
        private BlogContext _context; // DbContext: Tag, Post gibi DbSet'leri barındırır
        public EfTagRepository(BlogContext context)
        {
            _context = context;
        }
        // Sorgulanabilir Tag kaynağı döndürür (IQueryable gecikmeli çalışır)
        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag Tag)
        {
            // Yeni Tag'i ekleyip kaydeder
            _context.Tags.Add(Tag);
            _context.SaveChanges();
        }
    }
}