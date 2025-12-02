using System;

namespace Queene.Core.Exceptions
{
    public class FenValidationException : Exception
    {
        public FenValidationException()
        { }

        public FenValidationException(string message) : base(message)
        { }

        public FenValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
