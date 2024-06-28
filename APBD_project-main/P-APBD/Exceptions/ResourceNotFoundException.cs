namespace Projekt.Errors
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
    }
}