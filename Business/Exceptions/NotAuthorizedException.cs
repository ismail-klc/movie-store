using System;

namespace Business.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message = "Not authorized")
        : base(message) { }
    }
}