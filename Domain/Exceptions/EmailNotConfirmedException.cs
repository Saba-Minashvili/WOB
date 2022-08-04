namespace Domain.Exceptions
{
    public sealed class EmailNotConfirmedException : BadRequestException
    {
        public EmailNotConfirmedException(string message)
            :base(message)
        {

        }
    }
}
