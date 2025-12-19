using Queene.Core.Fen;
using Queene.Core.MovesGenerating;

namespace Queene.Core.Converters
{
    public interface IBitBoardContextConverter
    {
        BoardState Convert(BoardContext bitBoardContext);
    }
}
