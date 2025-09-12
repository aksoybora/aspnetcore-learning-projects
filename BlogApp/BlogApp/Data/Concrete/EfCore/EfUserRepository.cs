using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private BlogContext _context;
        // EF Core BlogContext enjekte edilir
        public EfUserRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<User> Users => _context.Users; // IQueryable olarak expose edilir

        public void CreateUser(User user)
        {
            // Basit ekleme işlemi ve kalıcı hale getirme
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}