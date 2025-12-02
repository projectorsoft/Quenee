using Queene.Core.Enums;
using Queene.Core.Models;

namespace Queene.Core.MovesGenerating.Pieces
{
    public interface IPiece
    {
        PieceTypeEnum PieceType { get; }

        void GenerateMoves(MoveGenerationTypeEnum generationType);
        void MakeMove(ExtendedMove move);
        void UnmakeMove(ExtendedMove move);
    }
}
