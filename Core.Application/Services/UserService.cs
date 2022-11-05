using AutoMapper;
using Core.Application.Dtos.Email;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : GenericService<User, UserViewModel, SaveUserViewModel>, IUserService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public override async Task<SaveUserViewModel> AddAsync(SaveUserViewModel vm)
        {

            SaveUserViewModel entityVM = await base.AddAsync(vm);

            //SaveViewModel entityVM = _mapper.Map<SaveViewModel>(entity);

            await _emailService.SendAsync(new EmailRequest
            {
                To = entityVM.Email,
                Subject = "Welcome!",
                Body = $"<h1 style="+"color: blue;"+">Welcome to Maycol Social Media App</h1>" +
                   $"<p>Your username is {entityVM.UserName}</p> \n" +
                   $"Link de activacion: " +
                   $"https://localhost:9999/User/ActivateUser?key={entityVM.ActivationKey}"
            });

            return entityVM;
        }


        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            loginVm.Password = PasswordEncryption.ComputeSha256Hash(loginVm.Password);

            return _mapper.Map<UserViewModel>(await _userRepository.Login(loginVm));
        }


        public async Task<List<UserViewModel>> GetAllWithIncludeAsync()
        {
            var list = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Friends", "Posts" });

            //var listX = await _userRepository.GetAllAsync();

            return list.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                ProfileImg = user.ProfileImg,
                Email = user.Email,
                UserName = user.UserName,
                Posts = user.Posts,
                Friends = user.Friends
            }).ToList();
        }

        public async Task<User> ExistUserValidation(SaveUserViewModel userVM)
        {
            UserViewModel userMap =  _mapper.Map<UserViewModel>(userVM);

            return await _userRepository.ExistUserValidation(userMap);
        }

        public async Task<SaveUserViewModel> ExistUserByActivationKey(string key)
        {
            SaveUserViewModel userMap = new();

            User founded = await _userRepository.ExistUserByActivationKey(key);

            if (founded != null)
            {
                 userMap = _mapper.Map<SaveUserViewModel>(founded);

            } else
            {
                userMap.UserName = null;
            }

            return userMap;

        }


        public async Task<bool> ActivateUser(SaveUserViewModel userVM)
        {
            var user =  await _userRepository.GetByIdAsync(userVM.Id);
            user.ActivationKey = null;
            user.Enabled = 1;

            await _userRepository.UpdateAsync(user, user.Id);

            return true;
        }


    }
}
