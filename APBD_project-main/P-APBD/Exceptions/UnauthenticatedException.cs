namespace Projekt.Errors
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message) : base(message) { }
    }
}