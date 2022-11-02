using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.FriendsVM;
using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Application.Services
{
    public class PostService : GenericService<Post, PostViewModel, SavePostViewModel>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentService _commentService;
        private readonly IFriendsService _friendsService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;
        public PostService(IPostRepository postRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            ICommentService commentService, IFriendsService friendsService) : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _commentService = commentService;
            _friendsService = friendsService;
        }

        public override async Task UpdateAsync(SavePostViewModel vm, int id)
        {
            Post postFinded = await _postRepository.GetByIdAsync(id);
            postFinded.Tittle = vm.Tittle;
            postFinded.Body = vm.Body;
            postFinded.ImagePost = vm.ImagePost == null ? postFinded.ImagePost : vm.ImagePost;

            await _postRepository.UpdateAsync(postFinded, id);
        }

        public async Task<List<PostViewModel>> GetAllWithIncludeAsyncFriends()
        {
            List<Post> list = await _postRepository.GetAllWithIncludeAsync(new List<string> { "User", "Comments" });

            List<Comment> listComment = await _commentService.GetAllWithIncludeAsync();

            List<FriendsViewModel> friendsList = await _friendsService.GetAllViewModel();

            var friendListX = friendsList.Where(p => p.UserId == _user.Id || p.FriendId == _user.Id).ToList();

            List<PostViewModel> listT = new();

            List<PostViewModel> listX = list.Select(post => new PostViewModel
            {
                Id = post.Id,
                Tittle = post.Tittle,
                Body = post.Body,
                ImagePost = post.ImagePost,
                User = post.User,
                Created = post.Created != null ? post.Created.Value.ToString("MMMM dd, yyyy") : null,
                LastModified = post.LastModified != null ? post.LastModified.Value.ToString("MMMM dd, yyyy") : null,
                Comments = listComment.Where(p => p.PostId == post.Id).ToList(),
            }).ToList();

            foreach (var item in friendListX)
            {
                if(item.UserId == _user.Id)
                {
                    listT = listX.Where(p => p.User.Id == item.FriendId).ToList();
                } else
                {
                    listX = listX.Where(p => p.User.Id == item.UserId).ToList();

                    foreach (var x in listX)
                    {
                        var post = new PostViewModel();
                        post.Id = x.Id;
                        post.Tittle = x.Tittle;
                        post.Body = x.Body;
                        post.ImagePost = x.ImagePost;
                        post.User = x.User;
                        post.Created = x.Created;
                        post.LastModified = x.LastModified;
                        post.Comments = x.Comments;

                        listT.Add(post);
                    }

                }

            }
            return listT;
        }

        public async Task<List<PostViewModel>> GetAllWithIncludeAsync()
        {
            List<Post> list = await _postRepository.GetAllWithIncludeAsync(new List<string> { "User", "Comments" });

            List<Comment> listComment = await _commentService.GetAllWithIncludeAsync();

            return list.Where(p => p.UserId == _user.Id).Select(post => new PostViewModel
            {
                Id = post.Id,
                Tittle = post.Tittle,
                Body = post.Body,
                ImagePost = post.ImagePost,
                User = post.User,
                Created = post.Created != null ? post.Created.Value.ToString("MMMM dd, yyyy") : null,
                LastModified = post.LastModified != null ? post.LastModified.Value.ToString("MMMM dd, yyyy") : null,
                Comments = listComment.Where(p => p.PostId == post.Id).ToList(),
            }).ToList();
        }
    }
}
