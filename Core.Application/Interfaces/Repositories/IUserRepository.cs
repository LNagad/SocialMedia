﻿using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Login(LoginViewModel userVM);
        Task<User> ExistUserValidation(UserViewModel userVM);
        Task<User> ExistUserByActivationKey(string key);
    }
}
