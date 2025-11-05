namespace BookstoreApplication.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class UserCreationException : Exception
    {
        public UserCreationException(string message) : base(message) { }
        public UserCreationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class GoogleAuthException : Exception
    {
        public GoogleAuthException(string message) : base(message) { }
        public GoogleAuthException(string message, Exception innerException) : base(message, innerException) { }
    }
}