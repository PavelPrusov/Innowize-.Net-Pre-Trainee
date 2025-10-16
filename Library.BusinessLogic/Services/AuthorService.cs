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
        private readonly IRepository<Author> _authorRepository;
        public AuthorService(IRepository<Author> repository)
        {
            _authorRepository = repository;
        }
        private AuthorDto MapToDto(Author author) =>
            new AuthorDto(author.Id, author.Name, author.DateOfBirth);

        private Author MapToEntity(CreateAuthorDto dto) =>
            new Author { Name = dto.Name, DateOfBirth = dto.DateOfBirth };
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto authorDto)
        {
            var author = MapToEntity(authorDto);

            var createdAuthor = await _authorRepository.AddAsync(author);

            var result = MapToDto(createdAuthor);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _authorRepository.GetByIdAsync(id);
            if (book == null) throw NotFoundException.AuthorNotFound(id);

            await _authorRepository.DeleteAsync(id);
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
    }
}
