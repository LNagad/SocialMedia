using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IGenericService<Entity, ViewModel, SaveViewModel>
        where Entity : class
        where ViewModel : class
        where SaveViewModel : class
    {
        Task<SaveViewModel> AddAsync(SaveViewModel vm);
        Task UpdateAsync(SaveViewModel vm, int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetViewModelById(int id);
        Task DeleteAsync(int id);
    }
}
