using System;

namespace Quenee.ConsoleApp.Commands.Exceptions
{
    public class CommandParserException : Exception
    {
        public const string Message = "Exception occured during parsing command. Check internal message.";
        public CommandParserException() : base(Message)
        {
        }

        public CommandParserException(Exception innerException) : base(Message, innerException)
        {
        }
    }
}
