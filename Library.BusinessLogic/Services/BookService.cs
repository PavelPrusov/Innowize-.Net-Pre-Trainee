using FluentValidation;
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
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IValidator<CreateBookDto> _createBookValidator;
        private readonly IValidator<UpdateBookDto> _updateBookValidator;

        private async Task ValidateAsync<T>(T dto, IValidator<T> validator)
        {
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IValidator<CreateBookDto> createValidator,
            IValidator<UpdateBookDto> updateValidator
            )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;

            _createBookValidator = createValidator;
            _updateBookValidator = updateValidator;
        }
        private BookDto MapToDto(Book book) =>
            new BookDto(book.Id, book.Title, book.PublishedYear, book.AuthorId);

        private Book MapToEntity(CreateBookDto dto) =>
            new Book { Title = dto.Title, PublishedYear = dto.PublishedYear, AuthorId = dto.AuthorId };
        public async Task<BookDto> CreateAsync(CreateBookDto bookDto)
        {
            await ValidateAsync(bookDto, _createBookValidator);
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

            await _bookRepository.DeleteAsync(book);
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
            await ValidateAsync(bookDto, _updateBookValidator);

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

        public async Task<List<BookDto>> GetFilteredBooksAsync(BookFilterDto filter)
        {
            var tasks = new List<Task<List<Book>>>();

            if (filter.AuthorId.HasValue)
                tasks.Add(_bookRepository.GetBooksByAuthorAsync(filter.AuthorId.Value));

            if (filter.PublishedAfter.HasValue)
                tasks.Add(_bookRepository.GetBooksPublishedAfterAsync(filter.PublishedAfter.Value));

            if (!string.IsNullOrEmpty(filter.TitlePart))
                tasks.Add(_bookRepository.SearchByTitlePartAsync(filter.TitlePart.Trim()));

            if (tasks.Count == 0)
            {
                var allBooks = await _bookRepository.GetAllAsync();
                return allBooks.Select(MapToDto).ToList();
            }

            var results = await Task.WhenAll(tasks);

            IEnumerable<Book> filteredBooks = results[0];

            for (int i = 1; i < results.Length; i++)
            {
                filteredBooks = filteredBooks.Intersect(results[i]);
            }

            return filteredBooks.Select(MapToDto).ToList();
        }
    }
}
