using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IBaseRepo<T> where T : class
    {
        Task<T> GetAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllPaginatedAsync(int page, int pageSize);
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task PhysicalDeleteAsync(T item);
    }
}
