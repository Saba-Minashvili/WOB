using Contracts;

namespace EmailSender.Services.Abstraction
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmailAsync(string? to);
        Task<bool> ConfirmEmailAsync(string? email, string? code);
        Task<bool> SendChangeEmailMessageAsync(ChangeEmailDto? changeEmailDto);
        Task<bool> ChangeEmailAsync(string? userId, string? newEmail, string? code);
    }
}
