using FluentValidation;
using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Library.DataAccess.Repositories;
using Library.Domain.Entities;

namespace Library.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IValidator<CreateAuthorDto> _createAuthorValidator;
        private readonly IValidator<UpdateAuthorDto> _updateAuthorValidator;
        public AuthorService(
            IAuthorRepository authorRepository,
            IValidator<CreateAuthorDto> createValidator,
            IValidator<UpdateAuthorDto> updateValidator
            )
        {
            _authorRepository = authorRepository;
            _createAuthorValidator = createValidator;
            _updateAuthorValidator = updateValidator;
        }

        private async Task ValidateAsync<T>(T dto, IValidator<T> validator)
        {
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
        private AuthorDto MapToDto(Author author) =>
            new AuthorDto(author.Id, author.Name, author.DateOfBirth);

        private Author MapToEntity(CreateAuthorDto dto) =>
            new Author { Name = dto.Name, DateOfBirth = dto.DateOfBirth };

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto authorDto)
        {
            await  ValidateAsync(authorDto, _createAuthorValidator);

            var author = MapToEntity(authorDto);

            var createdAuthor = await _authorRepository.AddAsync(author);

            var result = MapToDto(createdAuthor);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) throw NotFoundException.AuthorNotFound(id);

            await _authorRepository.DeleteAsync(author);
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();

            var result = authors.Select(MapToDto).ToList();

            return result;
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) throw NotFoundException.AuthorNotFound(id);

            var result = MapToDto(author);
            return result;
        }

        public async Task<AuthorDto> UpdateAsync(int id, UpdateAuthorDto authorDto)
        {
            await ValidateAsync(authorDto, _updateAuthorValidator);

            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null) throw NotFoundException.AuthorNotFound(id);

            var updatedAuthor = new Author
            {
                Id = id,
                DateOfBirth = authorDto.DateOfBirth,
                Name = authorDto.Name,
            };

            var updateResult = await _authorRepository.UpdateAsync(updatedAuthor);

            var result = MapToDto(updateResult);
            return result;
        }

     
        public async Task<List<AuthorDto>> GetFilteredAuthorsAsync(AuthorFilterDto filter)
        {
            var tasks = new List<Task<List<Author>>>();
     
            if (!string.IsNullOrEmpty(filter.NamePart))
                tasks.Add(_authorRepository.FindByNamePartAsync(filter.NamePart.Trim()));

            if (filter.HasBooks.HasValue && filter.HasBooks.Value)
                tasks.Add(_authorRepository.GetAuthorsWithBooksAsync());

            if (filter.MinBookCount.HasValue)
            {
                var authorsWithCounts = await _authorRepository.GetAuthorsWithBookCountAsync();
                var countFiltered = authorsWithCounts
                    .Where(x => x.BookCount >= filter.MinBookCount.Value)
                    .Select(x => x.Author)
                    .ToList();
                tasks.Add(Task.FromResult(countFiltered));
            }

            if (tasks.Count == 0)
            {
                var allAuthors = await _authorRepository.GetAllAsync();
                return allAuthors.Select(MapToDto).ToList();
            }

            var results = await Task.WhenAll(tasks);
            IEnumerable<Author> filteredAuthors = results[0];

            for (int i = 1; i < results.Length; i++)
            {
                filteredAuthors = filteredAuthors.Intersect(results[i]);
            }
            return filteredAuthors.Select(MapToDto).ToList();
        }
    }
 }

