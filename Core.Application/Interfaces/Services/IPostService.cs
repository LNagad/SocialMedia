using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<Post, PostViewModel, SavePostViewModel>
    {
        Task<List<PostViewModel>> GetAllWithIncludeAsync();
    }
}
