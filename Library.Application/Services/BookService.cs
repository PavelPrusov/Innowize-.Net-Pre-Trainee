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

        public async Task<BookDto> CreateAsync(CreateBookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var createdBook = await _repository.AddAsync(book);

            var result = new BookDto { 
                Id = createdBook.Id, 
                Title = createdBook.Title, 
                PublishedYear = createdBook.PublishedYear, 
                AuthorId = createdBook.AuthorId };

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();

            var result = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            }).ToList();

            return result;
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return null;

            var result =  new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            };

            return result;
        }

        public async Task<BookDto?> UpdateAsync(int id, UpdateBookDto bookDto)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null) return null;

            var updatedBook = new Book
            {
                Id = id,
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var updateResult = await _repository.UpdateAsync(updatedBook);
            if (updateResult == null) return null;

            var result = new BookDto
            {
                Id = updateResult.Id,
                Title = updateResult.Title,
                PublishedYear = updateResult.PublishedYear,
                AuthorId = updateResult.AuthorId
            };

            return result;
        }
    }
}
