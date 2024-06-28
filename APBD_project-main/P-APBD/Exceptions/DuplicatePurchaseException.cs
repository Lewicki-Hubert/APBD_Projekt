namespace Projekt.Exceptions
{
    public class DuplicatePurchaseException : Exception
    {
        public DuplicatePurchaseException(string message) : base(message) { }
    }
}