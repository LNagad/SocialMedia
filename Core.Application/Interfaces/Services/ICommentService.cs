using Core.Application.ViewModels.CommentVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<Comment, CommentViewModel, SaveCommentViewModel>
    {
        Task<List<Comment>> GetAllWithIncludeAsync();
    }
}
