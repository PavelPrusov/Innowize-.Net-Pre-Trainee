using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Interfaces;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public  async Task<ActionResult<List<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] BookFilterDto filter)
        {
            var tasks = await _bookService.GetFilteredBooksAsync(filter);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Add(CreateBookDto bookDto)
        {
            var newBook = await _bookService.CreateAsync(bookDto);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> Update(int id,UpdateBookDto bookDto)
        {
            var updatedBook = await _bookService.UpdateAsync(id,bookDto);
            return Ok(updatedBook);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           await _bookService.DeleteAsync(id);
           return NoContent();
        }
    }
}
