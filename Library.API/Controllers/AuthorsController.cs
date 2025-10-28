using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Interfaces;
using Library.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] AuthorFilterDto filter)
        {
            var tasks = await _authorService.GetFilteredAuthorsAsync(filter);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Add(CreateAuthorDto authorDto)
        {
            var author = await _authorService.CreateAsync(authorDto);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> Update(int id, UpdateAuthorDto authorDto)
        {
            var author = await _authorService.UpdateAsync(id, authorDto);
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
