// ICommentRepository: Yorum işlemleri için sorgulama ve ekleme sözleşmesini tanımlar.
using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);
    }
}