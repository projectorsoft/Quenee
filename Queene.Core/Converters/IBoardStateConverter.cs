using Queene.Core.Fen;

namespace Queene.Core.Converters
{
    public interface IBoardStateConverter
    {
        string PiecesPart { get; }
        string PlayerPart { get; }
        string CastlingsPart { get; }
        string EnPassantePart { get; }
        string HalfMovePart { get; }
        string FullMovePart { get; }

        string Convert(BoardState boardState);
    }
}
