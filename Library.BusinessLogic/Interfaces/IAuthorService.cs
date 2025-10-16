using Library.BusinessLogic.DTO.Author;

namespace Library.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetByIdAsync(int id);
        Task<AuthorDto> CreateAsync(CreateAuthorDto authorDto);
        Task<AuthorDto> UpdateAsync(int id, UpdateAuthorDto authorDto);
        Task DeleteAsync(int id);

    }
}
