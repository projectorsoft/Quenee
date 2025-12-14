using Quenee.ConsoleApp.Commands.Abstract;
using System;

namespace Quenee.ConsoleApp.Commands.PerftCommand
{
    public class PerftCommandRequest : CommandRequestBase
    {
        public int Ply { get; set; }
        public int MaxParallelOperations { get; set; } = Environment.ProcessorCount;
    }
}
