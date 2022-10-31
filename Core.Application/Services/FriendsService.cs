using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.FriendsVM;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class FriendsService : GenericService<Friends, FriendsViewModel, SaveFriendsViewModel>, IFriendsService
    {
        private readonly IFriendsRepository _friendsRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;
        public FriendsService(IFriendsRepository friendsRepo, IMapper mapper, IUserRepository userRepo,
            IHttpContextAccessor httpContextAccessor) : base(friendsRepo, mapper)
        {
            _friendsRepo = friendsRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<List<FriendsViewModel>> GetAllWithIncludeAsync()
        {
            var list = await _friendsRepo.GetAllWithIncludeAsync(new List<string> { "User" });


            return list.Select(friend => new FriendsViewModel
            {
                Id = friend.Id,
                UserName = friend.UserName,
                UserId = friend.UserId,
                FriendId = friend.FriendId,
                User = friend.User
            }).ToList();
        }

        public override async Task<SaveFriendsViewModel> AddAsync(SaveFriendsViewModel vm)
        {
            var user = new UserViewModel();
            user.UserName = vm.UserName;

            var userFinded = await _userRepo.ExistUserValidation(user);

            if (userFinded != null)
            {
                vm.FriendId = userFinded.Id;
                return await base.AddAsync(vm);
            } 
            else
            {
                return null;
            }
        }

    }
}
