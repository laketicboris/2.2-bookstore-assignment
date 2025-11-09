namespace BookstoreApplication.Exceptions
{
    public class ApiCommunicationException : Exception
    {
        public ApiCommunicationException(string message) : base(message)
        {
        }

        public ApiCommunicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}