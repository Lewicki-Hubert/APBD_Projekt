namespace Projekt.Exceptions
{
    public class EmptyResultException : Exception
    {
        public EmptyResultException(string message) : base(message) { }
    }
}