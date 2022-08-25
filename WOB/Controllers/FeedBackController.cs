using Contracts.FeedBack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedBackController : ControllerBase
    {
        private readonly IServiceManager? _serviceManager;

        public FeedBackController(IServiceManager? serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllFeedBacks(CancellationToken cancellationToken = default)
        {
            var feedBacks = await _serviceManager.FeedBackService.GetAllAsync(cancellationToken);

            if(feedBacks == null)
            {
                return BadRequest("Unable to get data of feedBacks.");
            }

            return Ok(feedBacks);
        }

        [HttpGet("[action]/{bookId}")]
        public async Task<IActionResult> GetFeedBacksByBookId(int bookId, CancellationToken cancellationToken = default)
        {
            if(bookId == 0)
            {
                return BadRequest($"{nameof(bookId)} cannot be 0.");
            }

            var feedBacks = await _serviceManager.FeedBackService.GetByBookIdAsync(bookId, cancellationToken);

            if(feedBacks == null)
            {
                return BadRequest("Unable to get data of feedBacks.");
            }

            return Ok(feedBacks);
        }

        [HttpGet("[action]/{feedBackId}")]
        public async Task<IActionResult> GetFeedBackById(int feedBackId, CancellationToken cancellationToken = default)
        {
            if(feedBackId == 0)
            {
                return BadRequest($"{nameof(feedBackId)} cannot be 0.");
            }

            var feedBack = await _serviceManager.FeedBackService.GetByIdAsync(feedBackId, cancellationToken);

            if(feedBack == null)
            {
                return BadRequest("Unable to get the feedBack.");
            }

            return Ok(feedBack);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedBack([FromBody] AddFeedBackDto? feedBackDto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.FeedBackService.CreateAsync(feedBackDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to add feedBack.");
            }

            return Ok();
        }

        [HttpPatch("{feedBackId}")]
        public async Task<IActionResult> UpdateFeedBack(int feedBackId, [FromBody] JsonPatchDocument<UpdateFeedBackDto>? feedBackDto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.FeedBackService.UpdateAsync(feedBackId, feedBackDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to update feedBack.");
            }

            return Ok();
        }

        [HttpDelete("{feedBackId}")]
        public async Task<IActionResult> DeleteFeedBack(int feedBackId, CancellationToken cancellationToken = default)
        {
            if(feedBackId == 0)
            {
                return BadRequest($"{nameof(feedBackId)} cannot be 0.");
            }

            var result = await _serviceManager.FeedBackService.DeleteAsync(feedBackId, cancellationToken);

            if (!result)
            {
                return BadRequest("Unable to delete feedBack.");
            }

            return Ok();
        }
    }
}
