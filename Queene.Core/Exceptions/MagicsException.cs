using System;

namespace Queene.Core.Exceptions
{
    public class MagicsException : Exception
	{
        public MagicsException()
        {
        }

        public MagicsException(string message) : base(message)
        {
        }

        public MagicsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
