namespace Projekt.Errors
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }
}