using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Middlewares;

namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUser;

        public UserController(IUserService userService, ValidateUserSession validateUser)
        {
            _userService = userService;
            _validateUser = validateUser;
         
        }

        public IActionResult Login()
        {
            if (_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            UserViewModel userVM = await _userService.Login(vm);

            if (userVM != null)
            {
                if (!userVM.Enabled)
                {
                    ModelState.AddModelError("userValidationEnabled", "Debe activar su usuario para poder iniciar sesion");

                    return View(vm);
                }
                    
                HttpContext.Session.Set<UserViewModel>("user", userVM);

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            } 
            else
            {
                ModelState.AddModelError("userValidation", "Datos de accesso incorrectos");
            }

            return View(vm);
        }

        public IActionResult Register()
        {
            if (_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            SaveUserViewModel userVm = await _userService.AddAsync(vm);

            if (userVm.Id != 0 && userVm != null)
            {
                string basePath = $"/Images/Users/{userVm.Id}";
            
                userVm.ProfileImg = UploadFileX.UploadFileTask(vm.File1, userVm.Id, basePath);

                await _userService.UpdateAsync(userVm, userVm.Id);
            }
            return RedirectToRoute( new { controller = "User", action = "Login"});
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");

            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
    }
}
