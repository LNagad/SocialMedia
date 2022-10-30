using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.CommentVM;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserViewModel _user;

        public CommentController(ICommentService commentService, IHttpContextAccessor httpContext)
        {
            _commentService = commentService;
            _contextAccessor = httpContext;
            _user = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        [HttpPost]
        public async Task<IActionResult> Comment(string comment, int postId)
        {
            SaveCommentViewModel Comment = new();
            Comment.PostId = postId;
            Comment.Content = comment;
            Comment.UserId = _user.Id;

            await _commentService.AddAsync(Comment);

            return RedirectToRoute(new {controller="Home", action="Index"});
        }
    }
}
