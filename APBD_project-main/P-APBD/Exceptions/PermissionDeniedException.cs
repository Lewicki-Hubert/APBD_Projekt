namespace Projekt.Errors
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(string message) : base(message) { }
    }
}