using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core.Domain.Entities;
using SocialMedia.Middlewares;

namespace SocialMedia.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel _user;
        private readonly ValidateUserSession _valSession;
        public PostsController(IPostService postService, IHttpContextAccessor httpContext, ValidateUserSession valSession)
        {
            _postService = postService;
            _httpContext = httpContext;
            _user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            _valSession = valSession;
        }

        public IActionResult Create()
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View("SavePost", new SavePostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePostViewModel vm)
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePost", vm);
            }
            vm.UserId = _user.Id;

            SavePostViewModel postVM = await _postService.AddAsync(vm);

            if(postVM != null && postVM.Id != 0)
            {
                if(vm.File != null)
                {
                    string basePath = $"/Images/Posts/{postVM.Id}";

                    postVM.ImagePost = UploadFileX.UploadFileTask(vm.File, postVM.Id, basePath);


                    await _postService.UpdateAsync(postVM, postVM.Id);
                }

            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SavePostViewModel vm =  await _postService.GetViewModelById(id);

            if (_user.Id != vm.UserId)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("SavePost", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePostViewModel vm)
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePost", vm);
            }

            if (vm != null && vm.Id != 0)
            {
                if(vm.File != null)
                {
                    var entityFinded =  await _postService.GetViewModelById(vm.Id);

                    string basePath = $"/Images/Posts/{vm.Id}";

                    if(entityFinded.ImagePost != null)
                    {

                        vm.ImagePost = UploadFileX.UploadFileTask(vm.File, vm.Id, basePath, true, entityFinded.ImagePost);
                    } else
                    {

                        vm.ImagePost = UploadFileX.UploadFileTask(vm.File, vm.Id, basePath,false, entityFinded.ImagePost);
                    }

                }
                await _postService.UpdateAsync(vm, vm.Id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SavePostViewModel vm = await _postService.GetViewModelById(id);

            if (_user.Id != vm.UserId)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SavePostViewModel vm)
        {
            if (!_valSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _postService.DeleteAsync(vm.Id);

            //--------------------Delete Image

            //get directory path
            string basePath = $"/Images/Posts/{vm.Id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true); // borrado recursivo, borrado completo
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
