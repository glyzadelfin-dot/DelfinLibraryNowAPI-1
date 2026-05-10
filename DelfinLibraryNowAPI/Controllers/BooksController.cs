using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DelfinLibraryNowAPI.Models;

namespace DelfinLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksCotroller : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {

           new Book { Id = 1, Title = "Little Women", Author = "Louisa May Alcott", Genre = " historical fiction", Available = true, PublishedYear = 2014 },
           new Book { Id = 2, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Available = true, PublishedYear = 2002 }

    };

        [HttpGet("{id}")]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrieved." });
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });
            return Ok(new { status = "success", data = book, message = "Book retrieved." });
        }

        [HttpPost("{id}")]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id },
            new { status = "success", data = newBook, message = "Book created." });

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book updated." });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found." });

            books.Remove(book);
            return Ok(new { status = "success", data = book, message = "Books Deleted" });
        }
    }
}
