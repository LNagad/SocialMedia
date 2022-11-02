using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Middlewares;
using SocialMedia.Models;
using System.Diagnostics;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserViewModel _user;
        private readonly ValidateUserSession _valSession;

        public HomeController(IHttpContextAccessor contextAccessor, IPostService postService, ICommentService commentService, ValidateUserSession valSession)
        {
            _contextAccessor = contextAccessor;
            _postService = postService;
            _user = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _commentService = commentService;
            _valSession = valSession;
        }

        public async Task<IActionResult> Index()
        {
            if(!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            ViewBag.Posts = await _postService.GetAllWithIncludeAsync();

            return View(new SavePostViewModel() );
        }
    }
}