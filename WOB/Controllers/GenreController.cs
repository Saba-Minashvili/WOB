using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetFictionGenres()
        {
            var genres = Enum.GetNames(typeof(FictionGenre)).ToList();

            if(genres == null)
            {
                return BadRequest("Unable to get the data of fiction genres");
            }

            return Ok(genres);
        }

        [HttpGet("[action]")]
        public IActionResult GetNonFictionGenres()
        {
            var genres = Enum.GetNames(typeof(NonfictionGenre)).ToList();

            if (genres == null)
            {
                return BadRequest("Unable to get the data of non-fiction genres");
            }

            return Ok(genres);
        }

        [HttpGet("[action]")]
        public IActionResult GetGenreSelection()
        {
            var selection = Enum.GetNames(typeof(GenreSelection)).ToList();

            if (selection == null)
            {
                return BadRequest("Unable to get the data of genres selection.");
            }

            return Ok(selection);
        }
    }
}
