using AutoMapper;
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
    public class FriendsRepository : GenericRepository<Friends>, IFriendsRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;
        public FriendsRepository(ApplicationContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
