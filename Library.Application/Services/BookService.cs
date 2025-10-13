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

        public async Task<BookDto> CreateAsync(CreateBookDto bookDto)
        {

            var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
            if (author == null)
                throw NotFoundException.AuthorNotFound(bookDto.AuthorId);

            var book = new Book
            {
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var createdBook = await _bookRepository.AddAsync(book);

            var result = new BookDto { 
                Id = createdBook.Id, 
                Title = createdBook.Title, 
                PublishedYear = createdBook.PublishedYear, 
                AuthorId = createdBook.AuthorId };

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw NotFoundException.BookNotFound(id);

            await _bookRepository.DeleteAsync(id);
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

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
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw NotFoundException.BookNotFound(id);

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
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null) throw NotFoundException.BookNotFound(id);


            if (existingBook.AuthorId != bookDto.AuthorId)
            {
                var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
                if (author == null)
                    throw NotFoundException.AuthorNotFound(bookDto.AuthorId);
            }

            var updatedBook = new Book
            {
                Id = id,
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var updateResult = await _bookRepository.UpdateAsync(updatedBook);

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
