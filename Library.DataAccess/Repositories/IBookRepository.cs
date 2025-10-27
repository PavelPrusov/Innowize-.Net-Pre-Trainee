using Library.Domain.Entities;

namespace Library.DataAccess.Repositories
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<List<Book>> GetBooksPublishedAfterAsync(int year);
        Task<List<Book>> SearchByTitlePartAsync(string titlePart);
        Task<List<Book>> GetBooksByAuthorAsync(int authorId);
    }
}
