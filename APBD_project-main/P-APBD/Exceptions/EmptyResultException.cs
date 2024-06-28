namespace Projekt.Errors
{
    public class EmptyResultException : Exception
    {
        public EmptyResultException(string message) : base(message) { }
    }
}