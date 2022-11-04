using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<User, UserViewModel, SaveUserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<User> ExistUserValidation(SaveUserViewModel userVM);
    }
}
