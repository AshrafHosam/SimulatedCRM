using Application.Contracts.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementation.Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly SimulatedCRMContext _context;
        public BaseRepo(SimulatedCRMContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task PhysicalDeleteAsync(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPaginatedAsync(int page, int pageSize)
        {
            return await _context.Set<T>().AsNoTracking().Skip((page - 1 * pageSize) < 0 ? 0 : (page - 1 * pageSize)).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<T> UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
