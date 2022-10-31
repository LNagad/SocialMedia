using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
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

        public FriendsController(IFriendsService friendsService, IHttpContextAccessor httpContextAccessor)
        {
            _friendsService = friendsService;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _friendsService.GetAllWithIncludeAsync());
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
