using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Core.Application.ViewModels.FriendsVM;
using Core.Application.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendsService _friendsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;
        private readonly IPostService _postService;

        public FriendsController(IFriendsService friendsService, IHttpContextAccessor httpContextAccessor, IPostService postService)
        {
            _friendsService = friendsService;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Friends = await _friendsService.GetAllWithIncludeAsync();

            return View(await _postService.GetAllWithIncludeAsyncFriends());
        }

        public IActionResult Create()
        {
            return View("SaveFriends", new SaveFriendsViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveFriendsViewModel vm)
        {
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
