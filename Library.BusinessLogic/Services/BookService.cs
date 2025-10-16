using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Library.DataAccess.Repositories;
using Library.Domain.Entities;

namespace Library.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Author> authorRepository) {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        private BookDto MapToDto(Book book) =>
            new BookDto(book.Id, book.Title, book.PublishedYear, book.AuthorId);

        private Book MapToEntity(CreateBookDto dto) =>
            new Book { Title = dto.Title, PublishedYear = dto.PublishedYear, AuthorId = dto.AuthorId };
        public async Task<BookDto> CreateAsync(CreateBookDto bookDto)
        {

            var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
            if (author == null) throw NotFoundException.AuthorNotFound(bookDto.AuthorId);

            var book = MapToEntity(bookDto);

            var createdBook = await _bookRepository.AddAsync(book);

            var result = MapToDto(createdBook);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw NotFoundException.BookNotFound(id);

            await _bookRepository.DeleteAsync(id);
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            var result = books.Select(MapToDto).ToList();

            return result;
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw NotFoundException.BookNotFound(id);

            var result = MapToDto(book);

            return result;
        }

        public async Task<BookDto> UpdateAsync(int id, UpdateBookDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null) throw NotFoundException.BookNotFound(id);


            if (existingBook.AuthorId != bookDto.AuthorId)
            {
                var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
                if (author == null) throw NotFoundException.AuthorNotFound(bookDto.AuthorId);
            }

            var updatedBook = new Book
            {
                Id = id,
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var updateResult = await _bookRepository.UpdateAsync(updatedBook);

            var result = MapToDto(updateResult);

            return result;
        }
    }
}
