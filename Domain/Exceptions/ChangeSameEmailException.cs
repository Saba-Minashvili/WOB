namespace Domain.Exceptions
{
    public sealed class ChangeSameEmailException : BadRequestException
    {
        public ChangeSameEmailException(string message)
            : base(message)
        {
        }
    }
}
