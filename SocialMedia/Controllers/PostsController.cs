using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PostVM;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View("SavePost", new SavePostViewModel());
        }

        [HttpPost]
        public IActionResult Create(SavePostViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("SavePost", vm);
            }




            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        //return RedirectToRoute(new { controller ="Home", action= "Index", SavePostViewModel = vm
    //});
        
    }
}
