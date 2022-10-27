using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : GenericService<User, UserViewModel, SaveUserViewModel>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository userRepository, IMapper mapper) :base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<UserViewModel> Login(LoginViewModel loginVm) 
        {
            loginVm.Password = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            
            return _mapper.Map<UserViewModel>(await _userRepository.Login(loginVm));
        }
    }
}
