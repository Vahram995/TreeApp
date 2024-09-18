using TreeApplication.Models;

namespace TreeApplication.Exceptions
{
    public class SecureException : ApplicationException
    {
        public SecureException() { }

        public SecureException(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}