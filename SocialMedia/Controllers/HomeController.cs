using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Diagnostics;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserViewModel _user;

        public HomeController(IHttpContextAccessor contextAccessor, IPostService postService)
        {
            _contextAccessor = contextAccessor;
            _postService = postService;
            _user = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetAllWithIncludeAsync());
        }
    }
}