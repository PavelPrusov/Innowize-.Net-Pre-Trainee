using Library.Domain.Entities;

namespace Library.DataAccess.Repositories
{
    public interface IAuthorRepository: IRepository<Author>
    {
        Task<Author?> FindByNameAsync(string namePart);
        Task<List<Author>> GetAuthorsWithBooksAsync();
        Task<List<(Author Author, int BookCount)>> GetAuthorsWithBookCountAsync();
    }
}
