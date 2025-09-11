using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    // Bu katman sadece "ne yapılacağı"nı tanımlar. Nasıl yapılacağı (EF Core vb.) bilinmez.
    // Amaç: Controller gibi üst katmanları veri erişim detaylarından bağımsız tutmak (loosely coupled).
    public interface IPostRepository
    {
        // Veritabanındaki Post kayıtlarına sorgulanabilir erişim sağlar.
        IQueryable<Post> Posts { get; }
        // Yeni bir Post eklemek için sözleşme (uygulama Concrete katmanda yapılır).
        void CreatePost(Post post);
    }
}