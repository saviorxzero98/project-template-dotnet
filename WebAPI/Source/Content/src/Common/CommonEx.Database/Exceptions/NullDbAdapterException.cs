namespace CommonEx.Database.Exceptions
{
    public class NullDbAdapterException : Exception
    {
        private const string DefaultMessage = "DbAdapter is null.";

        public NullDbAdapterException() : base(DefaultMessage)
        {

        }
        public NullDbAdapterException(string message) : base(message)
        {

        }
        public NullDbAdapterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
