using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _repository;
        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.Posts.ToList());
        }
    }
}