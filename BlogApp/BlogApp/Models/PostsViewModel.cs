// PostsViewModel: Liste sayfasında post ve etiketleri taşımak için kullanılan model.
using BlogApp.Entity;

namespace BlogApp.Models
{
    public class PostsViewModel
    {
        public List<Post> Posts { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}