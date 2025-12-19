namespace Queene.Core.MovesGenerating.Hashing
{
    public class PerftTTEntry
    {
        public ulong Hash { get; set; }
        public int Depth { get; set; }
        public ulong Nodes { get; set; }
    }
}
