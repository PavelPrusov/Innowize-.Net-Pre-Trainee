using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Interfaces;
using Library.DataAccess.Repositories;
using Library.Domain.Entities;

namespace Library.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        public BookService(IRepository<Book> repository) {
            _repository = repository;
        }

        public Task<BookDto> CreateAsync(CreateBookDto authorDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto?> UpdateAsync(int id, UpdateBookDto authorDto)
        {
            throw new NotImplementedException();
        }
    }
}
