namespace Projekt.Errors
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException(string message) : base(message) { }
    }
}