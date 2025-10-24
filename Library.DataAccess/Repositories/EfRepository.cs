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
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
             await _context.SaveChangesAsync();
            return entity;
        }
    }
}
