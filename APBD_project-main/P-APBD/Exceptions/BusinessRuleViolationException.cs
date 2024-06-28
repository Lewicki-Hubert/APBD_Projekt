namespace Projekt.Errors
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message) : base(message) { }
    }
}