using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.CommentVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class CommentService : 
        GenericService<Comment, CommentViewModel, SaveCommentViewModel>, ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepo) : base(commentRepository, mapper)
        {
            _commentRepo = commentRepository;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<List<Comment>> GetAllWithIncludeAsync()
        {
            var list = await _commentRepo.GetAllWithIncludeAsync(new List<string> { "Post" });

            var listX = await _userRepo.GetAllAsync();

            return list.Select(comt => new Comment
            {
                Id = comt.Id,
                PostId = comt.PostId,
                UserId = comt.UserId,
                Content = comt.Content,
                User = listX.Where(p => p.Id == comt.UserId).FirstOrDefault()
            }).ToList();
        }

    }
}
