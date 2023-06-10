namespace ETicaretAPI.Application.Exceptions
{
    public class PasswordChangedFailedException: Exception
    {
        public PasswordChangedFailedException() : base("Sifre degistirilemedi!")
        {
        }

        public PasswordChangedFailedException(string? message) : base(message)
        {
        }

        public PasswordChangedFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
