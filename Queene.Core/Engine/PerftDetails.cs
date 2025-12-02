namespace Queene.Core.Engine
{
    public class PerftDetails
    {
        public int Nodes { get; set; }
        public int Captures { get; set; }
        public int EnPassante { get; set; }
        public int Castles { get; set; }
        public int Promotions { get; set; }
        public int Checks { get; set; }
        public RootNode[] RootNodes { get; set; } = [];

        public void Clear()
        {
            Nodes = Captures = EnPassante = Castles = Promotions = Checks = 0;
        }
    }

    public class RootNode
    {
        public string Move { get; set; }
        public ulong Count { get; set; }
    }
}
