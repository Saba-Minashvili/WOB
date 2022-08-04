namespace Domain.Exceptions
{
    public sealed class EmailAlreadyConfirmedException : BadRequestException
    {
        public EmailAlreadyConfirmedException(string message)
            :base(message)
        {

        }
    }
}
