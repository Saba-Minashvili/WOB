using EmailSender.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailSender;

        public EmailController(IEmailService emailSender) => _emailSender = emailSender;

        [HttpPost("[action]/{to}")]
        public async Task<IActionResult> SendConfirmationEmail(string? to)
        {
            if (string.IsNullOrEmpty(to))
            {
                return BadRequest($"{nameof(to)} cannot be null or empty.");
            }

            var result = await _emailSender.SendConfirmationEmailAsync(to);

            if (!result)
            {
                return BadRequest("Unable to send email confirmation message.");
            }

            return Ok();
        }

        [HttpGet("[action]/{email}/{code}")]
        public async Task<IActionResult> ConfirmEmail(string? email, string? code)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest($"{nameof(email)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(code))
            {
                return BadRequest($"{nameof(code)} cannot be null or empty.");
            }

            var result = await _emailSender.ConfirmEmailAsync(email, code);

            if (!result)
            {
                return BadRequest("Unable to confirm the email.");
            }

            return Ok();
        }

        [HttpPost("[action]/{userId}/{newEmail}")]
        public async Task<IActionResult> SendChangeEmailConfirmation(string? userId, string? newEmail)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"{nameof(userId)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(newEmail))
            {
                return BadRequest($"{nameof(newEmail)} cannot be null or empty.");
            }

            var result = await _emailSender.SendChangeEmailMessageAsync(userId, newEmail);

            if (!result)
            {
                return BadRequest("Unable to send email change message.");
            }

            return Ok();
        }

        [HttpGet("[action]/{userId}/{newEmail}/{code}")]
        public async Task<IActionResult> ChangeEmail(string? userId, string? newEmail, string? code)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"{nameof(userId)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(newEmail))
            {
                return BadRequest($"{nameof(newEmail)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(code))
            {
                return BadRequest($"{nameof(code)} cannot be null or empty.");
            }

            var result = await _emailSender.ChangeEmailAsync(userId, newEmail, code);

            if (!result)
            {
                return BadRequest("Unable to change the email.");
            }

            return Ok();
        }
    }
}
