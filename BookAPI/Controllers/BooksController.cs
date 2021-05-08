using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBooks(int id)
        {
            return await bookRepository.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Books>> GetBooks()
        {
            return await bookRepository.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Books>> PostBooks([FromBody] Books book)
        {
            var newBook = await bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.ID}, newBook);
        }

        [HttpPut]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Books book)
        {
            if (id != book.ID)
            {
                return BadRequest();
            }

            await bookRepository.Update(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookToDelete = await bookRepository.Get(id);
            if (bookToDelete == null)
                return NotFound();

            await bookRepository.Delete(bookToDelete.ID);
            return NoContent();
        }
    }
}
