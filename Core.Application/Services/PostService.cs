using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class PostService : GenericService<Post, PostViewModel, SavePostViewModel>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;
        public PostService(IPostRepository postRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICommentService commentService) : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _commentService = commentService;
        }

        public override async Task UpdateAsync(SavePostViewModel vm, int id)
        {
            Post postFinded = await _postRepository.GetByIdAsync(id);
            postFinded.Tittle = vm.Tittle;
            postFinded.Body = vm.Body;
            postFinded.ImagePost = vm.ImagePost == null ? postFinded.ImagePost : vm.ImagePost;

            await _postRepository.UpdateAsync(postFinded, id);
        }

        public virtual async Task<List<PostViewModel>> GetAllWithIncludeAsync()
        {
            List<string> parameters = new () {"User", "Comments"};

            List<Post> list = await _postRepository.GetAllWithIncludeAsync(parameters);

            List<Comment> listComment = await _commentService.GetAllWithIncludeAsync();

            return list.Select(post => new PostViewModel
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
