using Queene.Core.Models;

namespace Queene.Core.MovesGenerating.PiecesList
{
    public interface IPiecesListService
    {
        void MakeMove(ExtendedMove move);
        void UnmakeMove(ExtendedMove move);
    }
}
