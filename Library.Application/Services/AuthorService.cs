using Library.BusinessLogic.DTO.Author;
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
        public Task<AuthorDto> CreateAsync(CreateAuthorDto authorDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuthorDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorDto?> UpdateAsync(int id, UpdateAuthorDto authorDto)
        {
            throw new NotImplementedException();
        }
    }
}
