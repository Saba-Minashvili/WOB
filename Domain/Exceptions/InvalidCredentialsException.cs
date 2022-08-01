namespace Domain.Exceptions
{
    public sealed class InvalidCredentialsException : BadRequestException
    {
        public InvalidCredentialsException()
            :base("Invalid Email or Password. Please try again.")
        {
        }
    }
}
