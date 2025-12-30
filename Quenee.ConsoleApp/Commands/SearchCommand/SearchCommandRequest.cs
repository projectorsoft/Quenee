using Quenee.ConsoleApp.Commands.Abstract;

namespace Quenee.ConsoleApp.Commands.SearchCommand
{
    public class SearchCommandRequest : CommandRequestBase
    {
        public int Ply {  get; set; }
    }
}
