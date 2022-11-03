using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Core.Application.ViewModels.FriendsVM;
using Core.Application.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Middlewares;

namespace SocialMedia.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendsService _friendsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;
        private readonly ValidateUserSession _valSession;
        private readonly IPostService _postService;


        public FriendsController(IFriendsService friendsService, IHttpContextAccessor httpContextAccessor, 
            IPostService postService, ValidateUserSession valSession)
        {
            _friendsService = friendsService;
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
            _valSession = valSession;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            ViewBag.Friends = await _friendsService.GetAllWithIncludeAsync();

            ViewBag.FriendsPosts = await _postService.GetAllWithIncludeAsyncFriends();

            return View(new SaveFriendsViewModel());
        }

        public IActionResult Create()
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View("SaveFriends", new SaveFriendsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveFriendsViewModel vm)
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveFriends", vm);
            }

            vm.UserId = _user.Id;
            var resultado = await _friendsService.AddAsync(vm);

            if(resultado == null)
            {
                ModelState.AddModelError("userExist", "Usuario no encontrado");
                return View("SaveFriends", vm);
            } 

            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }
    }
}
