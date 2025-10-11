using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Interfaces;
using Library.DataAccess.Repositories;
using Library.Domain.Entities;

namespace Library.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        public AuthorService(IRepository<Author> repository)
        {
            _repository = repository;
        }
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto authorDto)
        {
            var Author = new Author
            {
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };

            var createdAuthor = await _repository.AddAsync(Author);

            var result = new AuthorDto
            {
                Id = createdAuthor.Id,
                Name = createdAuthor.Name,
                DateOfBirth = createdAuthor.DateOfBirth,
            };
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
          return await _repository.DeleteAsync(id);
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();

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
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;

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
            var existingAuthor = await _repository.GetByIdAsync(id);
            if (existingAuthor == null) return null;

            var updatedAuthor = new Author
            {
                Id = id,
                DateOfBirth = authorDto.DateOfBirth,
                Name = authorDto.Name,
            };

            var updateResult = await _repository.UpdateAsync(updatedAuthor);
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
