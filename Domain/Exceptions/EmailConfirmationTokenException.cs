namespace Domain.Exceptions
{
    public sealed class EmailConfirmationTokenException : BadRequestException
    {
        public EmailConfirmationTokenException(string message)
            : base(message)
        {

        }
    }
}
