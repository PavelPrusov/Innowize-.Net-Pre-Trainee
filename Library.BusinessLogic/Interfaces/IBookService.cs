using Library.BusinessLogic.DTO.Book;

namespace Library.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<List<BookDto>> GetFilteredBooksAsync(BookFilterDto filter);
        Task<BookDto> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto bookDto);
        Task<BookDto> UpdateAsync(int id, UpdateBookDto bookDto);
        Task DeleteAsync(int id);
    }
}
