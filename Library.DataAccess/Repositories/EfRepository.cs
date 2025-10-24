using Library.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class, new()
    {
        private protected LibraryDbContext _context;
        private protected DbSet<T> _dbSet;
        public EfRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();

        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T?> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
