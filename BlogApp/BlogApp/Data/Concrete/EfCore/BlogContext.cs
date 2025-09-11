using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        // DbSet<T>: EF Core'un tablolarla eşleştirdiği koleksiyonlar
        public DbSet<Post> Posts => Set<Post>();     // Posts tablosu
        public DbSet<Comment> Comments => Set<Comment>(); // Comments tablosu
        public DbSet<Tag> Tags => Set<Tag>();        // Tags tablosu
        public DbSet<User> Users => Set<User>();     // Users tablosu
    }
}