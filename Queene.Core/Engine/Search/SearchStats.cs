using Queene.Core.Models;
using System.Collections.Generic;

namespace Queene.Core.Engine.Search
{
    public class SearchStats
    {
        public long NodesSearched { get; set; }
        public ExtendedMove[] BestMoves { get; set; } = new ExtendedMove[64];
        public List<ExtendedMove> PV { get; set; } = [];
        public int BestValue { get; set; }

        public override string ToString()
        {
            var pv = "";

            foreach (ExtendedMove move in PV)
                pv += $"{move},";

            return pv.Trim().TrimEnd(',');
        }
    }
}
