namespace Domain.Exceptions
{
    public sealed class AlreadyExistsException : BadRequestException
    {
        public AlreadyExistsException(string message)
            :base(message)
        {

        }
    }
}
