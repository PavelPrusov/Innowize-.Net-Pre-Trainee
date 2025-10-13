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
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto authorDto)
        {
            var Author = new Author
            {
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };

            var createdAuthor = await _authorRepository.AddAsync(Author);

            var result = new AuthorDto
            {
                Id = createdAuthor.Id,
                Name = createdAuthor.Name,
                DateOfBirth = createdAuthor.DateOfBirth,
            };
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _authorRepository.GetByIdAsync(id);
            if (book == null)
                throw NotFoundException.AuthorNotFound(id);

            await _authorRepository.DeleteAsync(id);
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();

            var result = authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                DateOfBirth= author.DateOfBirth,
                Name = author.Name,
            }).ToList();

            return result;
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) throw NotFoundException.AuthorNotFound(id);

            var result = new AuthorDto
            {
                Id = author.Id,
                DateOfBirth = author.DateOfBirth,
                Name = author.Name,
            };

            return result;
        }

        public async Task<AuthorDto?> UpdateAsync(int id, UpdateAuthorDto authorDto)
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
            if (updateResult == null) return null;

            var result = new AuthorDto
            {
                Id = updateResult.Id,
                DateOfBirth = updateResult.DateOfBirth,
                Name = updateResult.Name,
            };

            return result;
        }
    }
}
