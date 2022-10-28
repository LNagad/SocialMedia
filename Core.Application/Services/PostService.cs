using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PostVM;
using Core.Domain.Entities;
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
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IMapper mapper) : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        //public override async Task<SavePostViewModel> AddAsync(SavePostViewModel vm)
        //{
            

        //    return entityVM;
        //}
    }
}
