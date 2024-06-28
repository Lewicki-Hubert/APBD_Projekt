namespace Projekt.Errors
{
    public class DuplicatePurchaseException : Exception
    {
        public DuplicatePurchaseException(string message) : base(message) { }
    }
}