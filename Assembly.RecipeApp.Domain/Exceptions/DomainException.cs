namespace Assembly.RecipeApp.Domain.Exceptions
{
    internal class DomainException : Exception
    {
        public DomainException(string msg) : base(msg)
        {
        }
    }
}
