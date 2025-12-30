using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.SearchCommand
{
    public class SearchCommandResponse : CommandResponseBase
    {
        public long NodesCount { get; set; }
        public double TotalTimeInMs { get; set; }
    }
}
