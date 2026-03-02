using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.UndoMoveCommand
{
    public class UndoMoveCommandRequest : CommandRequestBase
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}
