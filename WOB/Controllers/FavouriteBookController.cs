using Contracts.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavouriteBookController : ControllerBase
    {
        private readonly IServiceManager? _serviceManager;

        public FavouriteBookController(IServiceManager? serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllFavouriteBooks(CancellationToken cancellationToken = default)
        {
            var favouriteBooks = await _serviceManager.FavouriteBookService.GetAllAsync(cancellationToken);

            if(favouriteBooks == null)
            {
                return BadRequest("Unable to get the data of favourite books.");
            }

            return Ok(favouriteBooks);
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetFavouriteBooksByUserId(string? userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"{nameof(userId)} cannot be null or empty.");
            }

            var favouriteBooks = await _serviceManager.FavouriteBookService.GetByUserIdAsync(userId, cancellationToken);

            if(favouriteBooks == null)
            {
                return BadRequest("Unable to get the data of favourite books.");
            }

            return Ok(favouriteBooks);
        }

        [HttpGet("[action]/{favouriteBookId}")]
        public async Task<IActionResult> GetFavouriteBookById(int favouriteBookId, CancellationToken cancellationToken)
        {
            if (favouriteBookId == 0)
            {
                return BadRequest($"{nameof(favouriteBookId)} cannot be equal to 0.");
            }

            var favouriteBook = await _serviceManager.FavouriteBookService.GetByIdAsync(favouriteBookId, cancellationToken);

            if (favouriteBook == null)
            {
                return BadRequest("Unable to get the favourite book.");
            }

            return Ok(favouriteBook);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavouriteBooks([FromBody] AddToFavouritesDto? favouriteBookDto, CancellationToken cancellationToken = default)
        {
            if (favouriteBookDto == null)
            {
                return BadRequest($"{nameof(favouriteBookDto)} cannot be null.");
            }

            var result = await _serviceManager.FavouriteBookService.AddToFavouritesAsync(favouriteBookDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to add book in favourites.");
            }

            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookFromFavourites(int bookId, CancellationToken cancellationToken = default)
        {
            if(bookId == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            var result = await _serviceManager.FavouriteBookService.DeleteFromFavouritesAsync(bookId, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to delete book from favourites.");
            }

            return Ok();
        }
    }
}
