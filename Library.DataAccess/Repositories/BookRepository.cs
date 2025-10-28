using Library.DataAccess.Data;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class BookRepository : EfRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            var result = await _dbSet.Where(b => b.AuthorId == authorId).ToListAsync();
            return result;
        }

        public async Task<List<Book>> GetBooksPublishedAfterAsync(int year)
        {
            var result = await _dbSet.Where(b => b.PublishedYear > year).ToListAsync();
            return result;
        }


        public async Task<List<Book>> SearchByTitlePartAsync(string titlePart)
        {
           var result = await _dbSet.Where(b=>b.Title.Contains(titlePart)).ToListAsync();
           return result;
        }
    }
}
