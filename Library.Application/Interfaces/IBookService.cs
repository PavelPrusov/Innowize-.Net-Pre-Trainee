using Library.BusinessLogic.DTO.Book;

namespace Library.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto authorDto);
        Task<BookDto?> UpdateAsync(int id, UpdateBookDto authorDto);
        Task<bool> DeleteAsync(int id);
    }
}
