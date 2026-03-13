using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Queen : Slider, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Queen;
        public const int Value = 900;

        public Queen(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
            : base(bitBoardContext, movesList, piecesListService, zorbistHash)
        {
        }

        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateMoves(MovesContainer.BishopMagics, generationType, SliderTypeEnum.Bishop);
            GenerateMoves(MovesContainer.RookMagics, generationType, SliderTypeEnum.Rook);
        }
    }
}
