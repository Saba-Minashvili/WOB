namespace EmailSender.Services.Abstraction
{
    public interface IEmailService
    {
        Task<bool> SendChangeEmailMessageAsync(string? userId, string? newEmail);
        Task<bool> SendConfirmationEmailAsync(string? to);
        Task<bool> ConfirmEmailAsync(string? email, string? code);
        Task<bool> ChangeEmailAsync(string? userId, string? newEmail, string? code);
    }
}
