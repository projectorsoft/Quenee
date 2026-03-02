using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.DoMoveCommand
{
    public class DoMoveCommandRequest : CommandRequestBase
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}
