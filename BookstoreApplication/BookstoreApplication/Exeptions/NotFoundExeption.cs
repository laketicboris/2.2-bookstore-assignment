namespace BookstoreApplication.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(int id) : base($"Resource with ID {id} was not found.")
        {
        }
    }
}