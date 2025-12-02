using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.PerftCommand
{
    public class PerftCommandRequest : CommandRequestBase
    {
        public override string Name => "Perft";

        public PerftCommandRequest(int ply)
        {
            _params.Add(new CommandParameter<int>("Ply", ply, true));
        }
    }
}
