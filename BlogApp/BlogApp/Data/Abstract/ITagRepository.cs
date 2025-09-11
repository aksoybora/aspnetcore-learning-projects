using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    // Abstract: Arayüz(Interface) ile bir sözleşme sunar. Uygulama detayı Concrete katmanda yapılır.
    public interface ITagRepository
    {
        // Tüm Tag kayıtlarına sorgulanabilir erişim sağlar.
        IQueryable<Tag> Tags { get; }
        // Yeni bir Tag eklemek için sözleşme.
        void CreateTag(Tag tag);
    }
}