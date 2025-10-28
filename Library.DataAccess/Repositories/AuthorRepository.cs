using Library.DataAccess.Data;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class AuthorRepository : EfRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<List<Author>> FindByNamePartAsync(string namePart)
        {
            var result =  await _dbSet.Where(a => a.Name.Contains(namePart)).ToListAsync();
            return result;
        }

        public async Task<List<(Author Author, int BookCount)>> GetAuthorsWithBookCountAsync()
        {
            var cortege = await _dbSet.Select(a => new { Author = a, BookCount = a.Books.Count }).ToListAsync();
            var result = cortege.Select(x => (x.Author, x.BookCount)).ToList();

            return result;
        }

        public async Task<List<Author>> GetAuthorsWithBooksAsync()
        {
            var result = await _dbSet.Where(a => a.Books.Any()).ToListAsync();
            return result;
        }
    }
}
