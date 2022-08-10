using Contracts.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class BookController : ControllerBase
    {
        private readonly IServiceManager? _serviceManager;

        public BookController(IServiceManager? serviceManager) => _serviceManager = serviceManager;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBooks(CancellationToken cancellationToken = default)
        {
            var books = await _serviceManager.BookService.GetAllAsync(cancellationToken);

            if (books == null)
            {
                return BadRequest("Unable to get data of books.");
            }

            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetBooksByName(string? name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"{nameof(name)} cannot be null or empty.");
            }

            var books = await _serviceManager.BookService.GetByNameAsync(name, cancellationToken);

            if (books == null)
            {
                return BadRequest("Unable to get data of books.");
            }

            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{genre}")]
        public async Task<IActionResult> GetBooksByGenre(string? genre, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest($"{nameof(genre)} cannot be null or empty.");
            }

            var books = await _serviceManager.BookService.GetByGenreAsync(genre, cancellationToken);

            if (books == null)
            {
                return BadRequest("Unable to get data of books.");
            }

            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string? author, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(author))
            {
                return BadRequest($"{nameof(author)} cannot be null or empty.");
            }

            var books = await _serviceManager.BookService.GetByAuthorAsync(author, cancellationToken);

            if (books == null)
            {
                return BadRequest("Unable to get data of books.");
            }

            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{pageNumber}")]
        public async Task<IActionResult> GetBooksByPageNumber(int pageNumber, CancellationToken cancellationToken = default)
        {
            if(pageNumber == 0)
            {
                return BadRequest("Number of pages cannot be 0.");
            }

            var books = await _serviceManager.BookService.GetByPageNumberAsync(pageNumber, cancellationToken);

            if (books == null)
            {
                return BadRequest("Unable to get data of books.");
            }

            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{releaseDate}")]
        public async Task<IActionResult> GetBooksByReleaseDate(string? releaseDate, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(releaseDate))
            {
                return BadRequest($"{nameof(releaseDate)} cannot be null or empty.");
            }

            var books = await _serviceManager.BookService.GetByReleaseDateAsync(releaseDate, cancellationToken);

            if (books == null)
            {
                return BadRequest("Unable to get data of books.");
            }

            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId, CancellationToken cancellationToken = default)
        {
            if (bookId == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            var book = await _serviceManager.BookService.GetByIdAsync(bookId, cancellationToken);

            if (book == null)
            {
                return BadRequest("Unable to get the book.");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto? bookDto, CancellationToken cancellationToken = default)
        {
            if(bookDto == null)
            {
                return BadRequest($"{nameof(bookDto)} cannot be null");
            }

            var result = await _serviceManager.BookService.CreateAsync(bookDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to add a book.");
            }

            return Ok();
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] UpdateBookDto? bookDto, CancellationToken cancellationToken = default)
        {
            if (bookId == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            if(bookDto == null)
            {
                return BadRequest($"{nameof(BookDto)} cannot be null");
            }

            var result = await _serviceManager.BookService.UpdateAsync(bookId, bookDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to update a book.");
            }

            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId, CancellationToken cancellationToken = default)
        {
            if(bookId == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            var result = await _serviceManager.BookService.DeleteAsync(bookId, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to delete a book.");
            }

            return Ok();
        }
    }
}
