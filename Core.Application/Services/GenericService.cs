using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class GenericService<Entity, ViewModel,SaveViewModel> : IGenericService<Entity, ViewModel,SaveViewModel>
        where Entity : class
        where ViewModel : class
        where SaveViewModel : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<SaveViewModel> AddAsync(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
           
            entity = await _repository.AddAsync(entity);

            SaveViewModel entityVM = _mapper.Map<SaveViewModel>(entity);

            return entityVM;
        }

        public async Task UpdateAsync(SaveViewModel vm, int id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            
            await _repository.UpdateAsync(entity, id);

        }
        public async Task<List<ViewModel>> GetAllViewModel()
        {
            var entityList = await _repository.GetAllAsync();

            return _mapper.Map<List<ViewModel>>(entityList);

        }

        public async Task<SaveViewModel> GetViewModelById(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);

            SaveViewModel SaveVM = _mapper.Map<SaveViewModel>(entity);

            return SaveVM;
        }

        public async Task DeleteAsync(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

    }
}
