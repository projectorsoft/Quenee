using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.SetPositionCommand
{
    public class SetPositionCommandRequest : CommandRequestBase
    {
        public string Fen {  get; set; }
    }
}
