using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.PerftCommand
{
    public class PerftCommandResponse : CommandResponseBase
    {
        public ulong NodesCount { get; set; }
        public double TotalTimeInMs { get; set; }
    }
}
