using Core.Application.ViewModels.FriendsVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IFriendsService : IGenericService<Friends, FriendsViewModel, SaveFriendsViewModel>
    {
        Task<List<FriendsViewModel>> GetAllWithIncludeAsync();
    }
}
