using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; } // Kullanıcılar üzerinde sorgu yapabilmek için
        void CreateUser(User User); // Yeni kullanıcı eklemek için basit metod
    }
}