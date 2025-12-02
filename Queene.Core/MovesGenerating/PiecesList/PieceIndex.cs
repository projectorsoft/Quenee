using Queene.Core.Enums;

namespace Queene.Core.MovesGenerating.PiecesList
{
    public class PieceIndex
    {
        public int Index { get; set; }

        public PieceTypeEnum PieceType { get; set; }

        public PieceIndex(PieceTypeEnum pieceType, int index)
        {
            Index = index;
            PieceType = pieceType;
        }

        public override string ToString()
        {
            return $"[{Index}] {PieceType}";
        }
    }
}
