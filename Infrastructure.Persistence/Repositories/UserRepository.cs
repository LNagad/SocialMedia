using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public override async Task<User> AddAsync(User user)
        {
            user.Password = PasswordEncryption.ComputeSha256Hash(user.Password);

            return await base.AddAsync(user);
        }

        public async Task<User> ExistUserValidation(UserViewModel userVM)
        {
            User user = await _dbContext.Set<User>().
                FirstOrDefaultAsync(user => user.UserName == userVM.UserName);

            return user;
        }

        public async Task<User> Login(LoginViewModel userVM)
        {
            User user = await _dbContext.Set<User>().
                FirstOrDefaultAsync(user => user.UserName == userVM.UserName
                && user.Password == userVM.Password);

            return user;
        }
    }
}
