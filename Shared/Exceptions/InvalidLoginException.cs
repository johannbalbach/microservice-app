namespace Shared.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Oops! We cant find your login, pls register first")
        {
        }

        public InvalidLoginException(string message) : base(message)
        {

        }
    }
}
